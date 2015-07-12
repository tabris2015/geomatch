using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {

	public int tiempoDeTransicion;
	private int cargaTransicion;
	private bool dragging = false;
	private Vector3 posAnterior;
	private string nombreB=" ",nombreA=" ";

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Enter");
		cargaTransicion = 0;
		//Guardando Coordenadas iniciales.
		if (other.tag == "Bandera") {
			posAnterior = other.transform.position;
			nombreB=other.gameObject.name;
		}
		// iniciar la animacion

	}

	void OnTriggerStay(Collider other) {
		nombreA = other.gameObject.name;
		if (other.tag == "Bandera"  && nombreA==nombreB) {
			cargaTransicion++;

			if (cargaTransicion > tiempoDeTransicion) {
				other.transform.position = new Vector3 (transform.position.x, transform.position.y,other.transform.position.z);
			}
		}
		return;

	}
	void OnTriggerExit(Collider other) {
		Debug.Log ("Saliendo");

		nombreA = other.gameObject.name;
		// Si se solto antes de tiempo lo coloca en su mismo lugar
		if (other.tag == "Bandera"  && nombreA==nombreB) {
			other.transform.position = posAnterior;
			cargaTransicion = 0;
		}
			// terminar la animacion
	}
}
