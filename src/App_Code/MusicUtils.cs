using System;
using System.Collections.Generic;

public class MusicUtils {

	public static IList<string> NOTAS = new List<string>( new string[] { "C", "D", "E", "F", "G", "A", "B" } );
	
	public static Random rnd = new Random();
	
	// Obtiene las alteraciones de una tonalidad mayor
	private static Dictionary<string, List<string>> alteraciones;
	
	static MusicUtils() {
		alteraciones = new Dictionary<string, List<string>>();
		// C
		alteraciones.Add("C", null);
		// G
        alteraciones.Add("G", new List<string>(new string[] { "F" }));
		// D
        alteraciones.Add("D", new List<string>(new string[] { "F", "C" }));
		// A
        alteraciones.Add("A", new List<string>(new string[] { "F", "C", "G" }));
		// E
        alteraciones.Add("E", new List<string>(new string[] { "F", "C", "G", "D" }));
		// B
        alteraciones.Add("B", new List<string>(new string[] { "F", "C", "G", "D", "A" }));
		// F#
        alteraciones.Add("F#", new List<string>(new string[] { "F", "C", "G", "D", "A", "E" }));
		// Gb
        alteraciones.Add("Gb", new List<string>(new string[] { "B", "E", "A", "D", "G", "C" }));
		// Db
        alteraciones.Add("Db", new List<string>(new string[] { "B", "E", "A", "D", "G" }));
		// Ab
        alteraciones.Add("Ab", new List<string>(new string[] { "B", "E", "A", "D" }));
		// Eb
        alteraciones.Add("Eb", new List<string>(new string[] { "B", "E", "A" }));
		// Bb
        alteraciones.Add("Bb", new List<string>(new string[] { "B", "E" }));
		// F
        alteraciones.Add("F", new List<string>(new string[] { "B" }));
	}
	
	public static void aplicarAlteraciones(string tonalidad, Nota[] notas) {
		string alteracion;
		if ("C".Equals(tonalidad))
			return;
		else if ("F".Equals(tonalidad) || tonalidad[tonalidad.Length - 1] == 'b')
			alteracion = "b";
		else
			alteracion = "#";
		List<string> notasAlteradas = alteraciones[tonalidad];
		foreach (Nota nota in notas) {
			if (notasAlteradas.IndexOf(nota.getNombre()) != -1)
				nota.setNombre(nota.getNombre() + alteracion);
		}
	}
	
	// Obtiene una lista de notas candidatas que pertenezcan al grado de la tonalidad y esté en la lista de disponibles
	public static IList<Nota> obtenerNotas(string tonalidad, int grado, Nota[] notasDisponibles) {
		IList<Nota> result = new List<Nota>();
		IList<string> acorde = new List<string>();
		// 1. Obtener las notas del acorde sobre el grado indicado
		// añadir la fundamental
		int idx = NOTAS.IndexOf(tonalidad.Substring(0, 1));
		idx += grado - 1;
		if (idx >= NOTAS.Count)
			idx -= NOTAS.Count;
		acorde.Add(NOTAS[idx]);
		// añadir la tercera
		idx += 2;
		if (idx >= NOTAS.Count)
			idx -= NOTAS.Count;
		acorde.Add(NOTAS[idx]);
		// añadir la quinta
		idx += 2;
		if (idx >= NOTAS.Count)
			idx -= NOTAS.Count;
		acorde.Add(NOTAS[idx]);
		// 2. Añadir al resultado las notas disponibles que pertenezcan al acorde
		foreach (Nota nota in notasDisponibles) {
			if (acorde.IndexOf(nota.getNombre().Substring(0, 1)) > -1) {
				result.Add(nota);
			}
		}
		return result;
	}
	
	// Calcula la longitud en tiempo de una figura, según su duración 
	public static double calcularLongitud(string duracion) {
		double result;
		bool puntillo = ('.' == duracion[duracion.Length - 1]);
		int dur;
		if (puntillo)
			dur = int.Parse(duracion.Substring(0, duracion.Length - 1));
		else 
			dur = int.Parse(duracion);
		result = 1.0d / dur;
		if (puntillo)
			result += result / 2;
		return result;
	}
	
	public static bool esCadenaVacia(string cadena) {
		return (cadena == null || cadena.Trim().Length == 0);
	}	
	
}
