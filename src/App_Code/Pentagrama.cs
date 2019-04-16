using System;
using System.Collections.Generic;

public class Pentagrama {
	
	private string clave;
	private Nota[] notasDisponibles;
	private BloqueRitmico[] bloquesRitmicosDisponibles;
	private int porcentajeSilencios;
	
	public Pentagrama(string tonalidad, string clave) {
		this.clave = clave;
        Configuracion cfg = Configuracion.obtenerConfiguracion();
		// Leer el rango de notas
		string rango = "G".Equals(clave) ? cfg.pentagrama_rango_g : cfg.pentagrama_rango_f;
		string[] notas = rango.Split(',');
		if (notas.Length != 2)
			throw new Exception("El rango de notas debe estar formado por dos notas. The range of notes must contain two notes");
		string notaIni = notas[0].Substring(0, 1);
		int octavaIni = int.Parse(notas[0].Substring(notas[0].Length - 1, 1));
		string notaFin = notas[1].Substring(0, 1);
		int octavaFin = int.Parse(notas[1].Substring(notas[1].Length - 1, 1));
		// calcular el número de notas
		int numNotas = MusicUtils.NOTAS.Count * (octavaFin - octavaIni + 1)
		             - MusicUtils.NOTAS.IndexOf(notaIni)
		             - (MusicUtils.NOTAS.Count - 1 - MusicUtils.NOTAS.IndexOf(notaFin));
		notasDisponibles = new Nota[numNotas];
		int idxNota = 0;
		for (int octava = octavaIni; octava <= octavaFin; octava++) {
			int idxNotaInicio = 0;
			int idxNotaFin = MusicUtils.NOTAS.Count - 1;
			if (octava == octavaIni)
				idxNotaInicio = MusicUtils.NOTAS.IndexOf(notaIni);
			if (octava == octavaFin)
				idxNotaFin = MusicUtils.NOTAS.IndexOf(notaFin);
			for (int i = idxNotaInicio; i <= idxNotaFin; i++) {
				notasDisponibles[idxNota] = new Nota(MusicUtils.NOTAS[i], octava, null);
				idxNota++;
			}
		}
		MusicUtils.aplicarAlteraciones(tonalidad, notasDisponibles);
		// Bloques rítmicos a utilizar
		string sRitmos = "G".Equals(clave) ? cfg.bloques_ritmicos_g : cfg.bloques_ritmicos_f;
		string[] sListaRimtos = sRitmos.Split(',') ;
		bloquesRitmicosDisponibles = new BloqueRitmico[sListaRimtos.Length];
		for (int i = 0; i < sListaRimtos.Length; i++) {
			string[] duraciones = sListaRimtos[i].Split('+');
			bloquesRitmicosDisponibles[i] = new BloqueRitmico(duraciones);
		}
	}
	
	public IList<Elemento> generarCompas(Compas compas, string tonalidad, int grado) {
		IList<Elemento> result = new List<Elemento>();
		// generar el ritmo
		double longitudCompas = compas.getLongitud();
		IList<BloqueRitmico> bloques = new List<BloqueRitmico>();
		double longitud = 0;
		longitud = generarBloquesRitmicos(bloques, longitud, longitudCompas);
		if (longitud < longitudCompas)
			throw new Exception("Con los bloques rítmicos disponibles no se puede rellenar el compás. With the rythm blocks available it is not possible to fill the measure");
		// generar la melodía
		IList<Nota> notasCandidatas = MusicUtils.obtenerNotas(tonalidad, grado, notasDisponibles);
		foreach (BloqueRitmico bloque in bloques) {
			string[] duraciones = bloque.getDuraciones();
			foreach (string duracion in duraciones) {
				Nota notaCandidata = notasCandidatas[MusicUtils.rnd.Next(notasCandidatas.Count)];
				Nota nota = new Nota(notaCandidata.getNombre(), notaCandidata.getOctava(), duracion);
				result.Add(nota);
			}
		}
		return result;
	}
	
	private double generarBloquesRitmicos(IList<BloqueRitmico> bloques, double longitudActual, double longitudObjetivo) {
		// 1. Obtenemos la lista de bloques candidatos
		// 2. Eliminamos de la lista aquellos bloques cuya longitud exceda el límite
		// 3. Mientras queden bloques candidatos y longitudActual < longitudObjetivo
		// 3.1. Elegir al azar uno de los bloques candidatos de los que quedan y añadirlo a 'bloques' incrementando la 'longitudActual'
		// 3.2. Invocar recursivamente a 'generarBloquesRitmicos'
		// 3.3. Si longitudActual < longitudObjetivo
		// 3.3.1. Eliminamos de la lista de bloques candidatos y de bloques el último elegido, decrementando 'longitudActual'

		// 1. Obtenemos la lista de bloques candidatos
		IList<BloqueRitmico> bloquesCandidatos = new List<BloqueRitmico>();
		for (int i = 0; i < bloquesRitmicosDisponibles.Length; i++)
			bloquesCandidatos.Add(bloquesRitmicosDisponibles[i]);
		// 2. Eliminamos de la lista aquellos bloques cuya longitud exceda el límite
		int j = 0;
		while (j < bloquesCandidatos.Count) {
			if (bloquesCandidatos[j].getLongitud() > longitudObjetivo - longitudActual)
				bloquesCandidatos.RemoveAt(j);
			else
				j++;
		}
		// 3. Mientras queden bloques candidatos y longitudActual < longitudObjetivo
		while (longitudActual < longitudObjetivo && bloquesCandidatos.Count > 0) {
			// 3.1. Elegir al azar uno de los bloques candidatos de los que quedan y añadirlo a 'bloques' incrementando la 'longitudActual'
			int idxBloque = MusicUtils.rnd.Next(bloquesCandidatos.Count);
			BloqueRitmico bloque = bloquesCandidatos[idxBloque];
			bloques.Add(bloque);
			longitudActual += bloque.getLongitud();
			if (longitudActual < longitudObjetivo) {
				// 3.2. Invocar recursivamente a 'generarBloquesRitmicos'
				longitudActual = generarBloquesRitmicos(bloques, longitudActual, longitudObjetivo);
				// 3.3. Si longitudActual < longitudObjetivo
				if (longitudActual < longitudObjetivo) {
					// 3.3.1. Eliminamos de la lista de bloques candidatos y de bloques el último elegido, decrementando 'longitudActual'
					bloquesCandidatos.RemoveAt(idxBloque);
					bloques.RemoveAt(bloques.Count - 1);
					longitudActual -= bloque.getLongitud(); 
				}
			}
		}
		return longitudActual;
	}
	
	public string getClave() {
		return clave;
	}
	
	public void setClave(string clave) {
		this.clave = clave;
	}
	
	public Nota[] getNotasDisponibles() {
		return notasDisponibles;
	}
	
	public void setNotasDisponibles(Nota[] notasDisponibles) {
		this.notasDisponibles = notasDisponibles;
	}
	
	public BloqueRitmico[] getBloquesRitmicosDisponibles() {
		return bloquesRitmicosDisponibles;
	}
	
	public void setBloquesRitmicosDisponibles(BloqueRitmico[] bloquesRitmicosDisponibles) {
		this.bloquesRitmicosDisponibles = bloquesRitmicosDisponibles;
	}
	
	public int getPorcentajeSilencios() {
		return porcentajeSilencios;
	}
	
	public void setPorcentajeSilencios(int porcentajeSilencios) {
		this.porcentajeSilencios = porcentajeSilencios;
	}

}
