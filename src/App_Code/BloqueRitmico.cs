using System.Text;

public class BloqueRitmico {

	private string[] duraciones;
	private double longitud;
	
	public BloqueRitmico(string[] duraciones) {
		this.duraciones = duraciones;
		actualizarLongitud();
	}
	
	private void actualizarLongitud() {
		longitud = 0;
		foreach (string duracion in duraciones)
            longitud += MusicUtils.calcularLongitud(duracion);
	}

	public string[] getDuraciones() {
		return duraciones;
	}

	public void setDuraciones(string[] duraciones) {
		this.duraciones = duraciones;
		actualizarLongitud();
	}
	
	public double getLongitud() {
		return longitud;
	}

	public string toString() {
		StringBuilder result = new StringBuilder();
		for (int i = 0; i < duraciones.Length; i++) {
			result.Append(duraciones[i]);
			if (i < duraciones.Length - 1)
				result.Append("+");
		}
		return result.ToString();
	}

}
