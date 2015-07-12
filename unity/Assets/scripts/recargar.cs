using UnityEngine;
using System.Collections;

public class recargar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void salir()
	{
		Application.Quit();
	}

	public void inicio()
	{
		Application.LoadLevel(0);
	}

}
