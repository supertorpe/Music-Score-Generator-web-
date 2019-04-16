using System.Collections.Generic;
using System.IO;

public interface Generador {
	
	void generar(IList<Elemento>[][] score, string tonalidad, string compas, Pentagrama[] pentagramas, StreamWriter writer);

}
