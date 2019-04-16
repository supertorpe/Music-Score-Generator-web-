using System.Text;

public class Nota : Elemento {

	private string nombre;
	private int octava;
	
	public Nota(string nombre, int octava, string duracion) : base(duracion) {
		this.nombre = nombre;
		this.octava = octava;
	}
	
	public string getNombre() {
		return nombre;
	}
	public void setNombre(string nombre) {
		this.nombre = nombre;
	}
	public int getOctava() {
		return octava;
	}
	public void setOctava(int octava) {
		this.octava = octava;
	}
	
	public string toString() {
		StringBuilder result = new StringBuilder();
		result.Append(nombre).Append(octava);
		if (duracion != null)
			result.Append("(").Append(duracion).Append(")");
		return result.ToString();
	}
}
