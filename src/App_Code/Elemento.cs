public class Elemento {

	protected string duracion;
	protected double longitud;
	
	public Elemento(string duracion) {
		this.duracion = duracion;
		actualizarLongitud();
	}

	private void actualizarLongitud() {
		if (duracion == null)
			longitud = 0;
		else
			longitud += MusicUtils.calcularLongitud(duracion);
	}
	
	public string getDuracion() {
		return duracion;
	}

	public void setDuracion(string duracion) {
		this.duracion = duracion;
		actualizarLongitud();
	}
	
	public double getLongitud() {
		return longitud;
	}

}
