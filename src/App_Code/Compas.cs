public class Compas {

	private string nombre;
	private double longitud;
	
	public Compas(string nombre) {
		this.nombre = nombre;
		actualizarLongitud();
	}
	
	private void actualizarLongitud() {
		string[] valores = nombre.Split('/');
		double numerador = double.Parse(valores[0]);
        double denominador = double.Parse(valores[1]);
		longitud = numerador / denominador;
	}
	
	public string getNombre() {
		return nombre;
	}

	public void setNombre(string nombre) {
		this.nombre = nombre;
		actualizarLongitud();
	}

	public double getLongitud() {
		return longitud;
	}
	
}
