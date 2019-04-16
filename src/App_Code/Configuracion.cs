using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Configuracion
{
    private const string SESSION_CFG_KEY = "_msg_cfg_";

    public string compas;
    public string tonalidad;
    public string compases;
    public string pentagramas = "G,F";
    public string pentagrama_rango_g;
    public string pentagrama_rango_f;
    public string bloques_ritmicos_g;
    public string bloques_ritmicos_f;
    public string silencios_g;
    public string silencios_f;
    public string progresiones_acordes = "1:2-3-4-5-6,2:3-5,3:4-6,4:1-2-5,5:1-3-6,6:2-4";

    public static Configuracion obtenerConfiguracion()
    {
        Configuracion result = null;
        System.Web.SessionState.HttpSessionState sid = HttpContext.Current.Session;
        result = (Configuracion)sid[SESSION_CFG_KEY];
        if (result == null)
        {
            result = new Configuracion();
            sid[SESSION_CFG_KEY] = result;
        }
        return result;
    }

    public static string getWebConfigParam(string key)
    {
        return System.Configuration.ConfigurationManager.AppSettings.Get(key);
    }

    public Configuracion()
	{
	}
}
