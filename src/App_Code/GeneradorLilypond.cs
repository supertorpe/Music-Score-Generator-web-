using System.Text;
using System.IO;
using System.Collections.Generic;

public class GeneradorLilypond : Generador {
	
	private static string[] NOMBRE_PENTAGRAMAS = { "staffA", "staffB", "staffC", "staffD", "staffE", "staffF", "staffG" };
	private static string NEWLINE = "\n";

	private static string mapearNota(string nota) {
		if (nota.Length > 1) {
			if (nota[1] == '#')
				nota = nota[0] + "is";
			else
				nota = nota[0] + "es";
		}
		return nota.ToLower();
	}
	
	private static string mapearOctava(int octava) {
		string result = "";
		if (octava == 3)
			return result;
		else if (octava < 3) {
			int numComas = 3 - octava;
			for (int i = 0; i < numComas; i++)
				result += ",";
		} else {
			int numComas = octava - 3;
			for (int i = 0; i < numComas; i++)
				result += "'";
		}
		return result;
	}
	
	public void generar(IList<Elemento>[][] score, string tonalidad, string compas, Pentagrama[] pentagramas, StreamWriter writer) {
		/*
		for (int j = 0; j < score.length; j++) {
			for (int i = 0; i < score[j].length; i++) {
				System.out.println("score[" + j + "][" + i + "]=" + score[j][i]);
			}
		}
		*/
		StringBuilder[] textoPentagramas = new StringBuilder[score.Length];
		for (int i = 0; i < score.Length; i++) {
			textoPentagramas[i] = new StringBuilder();
			// staffA = {
			textoPentagramas[i].Append(NOMBRE_PENTAGRAMAS[i]).Append(" = {").Append(NEWLINE);
			// \clef treble
			if ("G".Equals(pentagramas[i].getClave()))
				textoPentagramas[i].Append("\t\\clef treble").Append(NEWLINE);
			else if ("F".Equals(pentagramas[i].getClave()))
				textoPentagramas[i].Append("\t\\clef bass").Append(NEWLINE);
			// \key c \major
			string key = mapearNota(tonalidad);
			textoPentagramas[i].Append("\t\\key ").Append(key).Append(" \\major").Append(NEWLINE);
			// \time 3/4
			textoPentagramas[i].Append("\t\\time ").Append(compas).Append(NEWLINE);
			for (int j = 0; j < score[i].Length; j++) {
				// c'4 d'4 e'4 f'4 g'4 a'4 b'4 |
				IList<Elemento> elementos = score[i][j];
				textoPentagramas[i].Append("\t");
				foreach (Elemento elemento in elementos) {
					if (elemento is Silencio) {
						textoPentagramas[i].Append("R").Append(elemento.getDuracion()).Append(" ");
					} else if (elemento is Nota) {
						Nota nota = elemento as Nota;
						textoPentagramas[i].Append(mapearNota(nota.getNombre())).Append(mapearOctava(nota.getOctava())).Append(nota.getDuracion()).Append(" ");
					}
				}
				textoPentagramas[i].Append("|").Append(NEWLINE);
			}
			// \bar "|."
			textoPentagramas[i].Append("\t\\bar \"|.\"").Append(NEWLINE);
			// }
			textoPentagramas[i].Append("}").Append(NEWLINE).Append(NEWLINE);
		}
		foreach (StringBuilder textoPentagrama in textoPentagramas) {
			writer.WriteLine(textoPentagrama.ToString());
		}
		// \score {
        writer.WriteLine("\\score {");
		// \new TheStaff <<
        writer.WriteLine("\t\\new TheStaff <<");
		for (int i = 0; i < score.Length; i++) {
			string staffName = NOMBRE_PENTAGRAMAS[i]; 
		    // \new Staff = "staffA" \staffA
            writer.WriteLine("\t\t\\new Staff = \"" + staffName + "\" \\" + staffName);
		}
		// >>
        writer.WriteLine("\t>>");
		// \layout { }
        writer.WriteLine("\t\\layout { }");
		// }
        writer.WriteLine("}");
	}

}
