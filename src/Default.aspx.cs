using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Diagnostics;

public partial class _Default : System.Web.UI.Page 
{
    protected int compases;
    protected Compas compas;
    protected Pentagrama[] pentagramas;
    protected Dictionary<int, IList<int>> progresiones;
    protected Dictionary<int, IList<int>> progresionesInversas;
    protected IList<Elemento>[][] score;
    Configuracion cfg;

    protected void Page_Load(object sender, EventArgs e)
    {
        cfg = Configuracion.obtenerConfiguracion();
    }
    protected void cargarConfiguracion()
    {
        cfg.compas = ddlCompas.SelectedValue;
        cfg.tonalidad = rblTonalidad.SelectedValue;
        cfg.compases = tbCompases.Text;
        cfg.pentagrama_rango_g = ddlRangoG1A.SelectedValue + ddlRangoG1B.SelectedValue + "," + ddlRangoG2A.SelectedValue + ddlRangoG2B.SelectedValue;
        cfg.pentagrama_rango_f = ddlRangoF1A.SelectedValue + ddlRangoF1B.SelectedValue + "," + ddlRangoF2A.SelectedValue + ddlRangoF2B.SelectedValue;
        StringBuilder aux = new StringBuilder();
        foreach (ListItem item in cblBloquesRitmicosG.Items)
        {
            if (item.Selected)
            {
                if (aux.Length > 0)
                    aux.Append(",");
                aux.Append(item.Value);
            }
        }
        cfg.bloques_ritmicos_g = aux.ToString();
        if (cfg.bloques_ritmicos_g.Length == 0)
            throw new Exception("You must select one or more rythm blocks for the treble cleff.");
        aux = new StringBuilder();
        foreach (ListItem item in cblBloquesRitmicosF.Items)
        {
            if (item.Selected)
            {
                if (aux.Length > 0)
                    aux.Append(",");
                aux.Append(item.Value);
            }
        }
        cfg.bloques_ritmicos_f = aux.ToString();
        if (cfg.bloques_ritmicos_f.Length == 0)
            throw new Exception("You must select one or more rythm blocks for the bass cleff.");
        string[] sListaPentagramas = cfg.pentagramas.Split(',');
        pentagramas = new Pentagrama[sListaPentagramas.Length];
        for (int i = 0; i < sListaPentagramas.Length; i++)
        {
            pentagramas[i] = new Pentagrama(cfg.tonalidad, sListaPentagramas[i]);
        }
        compas = new Compas(cfg.compas);
        compases = int.Parse(cfg.compases);
        progresiones = new Dictionary<int, IList<int>>();
        progresionesInversas = new Dictionary<int, IList<int>>();
        string[] sListaProgresiones = cfg.progresiones_acordes.Split(',');
        for (int i = 0; i < sListaProgresiones.Length; i++)
        {
            string[] sValores = sListaProgresiones[i].Split(':');
            string sOrigen = sValores[0];
            string[] sDestinos = sValores[1].Split('-');
            int key = int.Parse(sOrigen);
            IList<int> value = new List<int>();
            for (int j = 0; j < sDestinos.Length; j++)
            {
                int val = int.Parse(sDestinos[j]);
                value.Add(val);
                IList<int> inversa = null;
                if (progresionesInversas.ContainsKey(val))
                    inversa = progresionesInversas[val];
                if (inversa == null)
                {
                    inversa = new List<int>();
                    inversa.Add(key);
                    progresionesInversas.Add(val, inversa);
                }
                else if (inversa.IndexOf(key) < 0)
                {
                    inversa.Add(key);
                }
            }
            progresiones.Add(key, value);
        }
    }

	protected void ejecutar() {
		// se va a crear un tablero: las filas son los pentagramas y las columnas son los compases
		// inicializamos el tablero
        score = new IList<Elemento>[pentagramas.Length][];
		// generamos los acordes, terminando en el grado I
		int[] acordes = generarAcordes();
		// generamos las partituras compás a compás
		for (int i = 0; i < compases; i++) {
			for (int j = 0; j < pentagramas.Length; j++) {
                if (i == 0)
                    score[j] = new IList<Elemento>[compases];
				score[j][i] = pentagramas[j].generarCompas(compas, cfg.tonalidad, acordes[i]);
			}
		}
		// generar el fichero de salida
        FileInfo fiLy = null;
        FileInfo fiPdf = null;
        string dir = Server.MapPath("~/App_Data/");
        string guid = Guid.NewGuid().ToString();
        string fichero = dir + guid + ".ly";
        string ficheroPdf = dir + guid + ".pdf";
        try
        {
            // Generar el .ly
            StreamWriter printer = null;
            try
            {
                printer = new StreamWriter(fichero);
                Generador generador = new GeneradorLilypond();
                generador.generar(score, cfg.tonalidad, compas.getNombre(), pentagramas, printer);
            }
            finally
            {
                if (printer != null)
                    printer.Close();
            }
            fiLy = new FileInfo(@fichero);
            if ("Lilypond".Equals(ddlFormato.SelectedValue))
            {
                // Devolver el .ly
                Response.AddHeader("Content-disposition", "attachment; filename=MusicScoreGen.ly");
                Response.AddHeader("Content-Length", fiLy.Length.ToString());
                Response.ContentType = "text/pplain";
                Response.WriteFile(fichero);
                Response.Flush();
                Response.End();
            }
            else
            {
                // Generar el .pdf
                string lilypondExec = Configuracion.getWebConfigParam("lilypond.exec");
                Process lilypond = new Process();
                lilypond.StartInfo.FileName = lilypondExec;
                lilypond.StartInfo.Arguments = "-o " + dir + " " + fichero;
                lilypond.StartInfo.WorkingDirectory = dir;
                lilypond.StartInfo.UseShellExecute = true;
                lilypond.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //lilypond.StartInfo.RedirectStandardOutput = true;
                //lilypond.StartInfo.RedirectStandardError = true;
                lilypond.Start();
                //string output = lilypond.StandardOutput.ReadToEnd();
                //string outputErr = lilypond.StandardError.ReadToEnd();
                lilypond.WaitForExit();
                fiPdf = new FileInfo(@ficheroPdf);
                // Devolver el .pdf
                Response.AddHeader("Content-disposition", "attachment; filename=MusicScoreGen.pdf");
                Response.AddHeader("Content-Length", fiPdf.Length.ToString());
                Response.ContentType = "application/pdf";
                Response.WriteFile(ficheroPdf);
                Response.Flush();
                Response.End();
            }
        }
        finally
        {
            if (fiLy != null && fiLy.Exists)
                fiLy.Delete();
            if (fiPdf != null && fiPdf.Exists)
                fiPdf.Delete();
        }
	}
	
	private int[] generarAcordes() {
		int[] result = new int[compases];
		result[compases - 1] = 1;
		for (int i = compases - 2; i >= 0; i--) {
			IList<int> candidatos = progresionesInversas[result[i + 1]];
			result[i] = candidatos[MusicUtils.rnd.Next(candidatos.Count)];
		}
		return result;
	}

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            lblMensaje.Text = "";
            cargarConfiguracion();
            ejecutar();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}
