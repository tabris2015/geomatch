using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;


public class Serial : MonoBehaviour {
	public float smooth = 2.0F;
	public float tiltAngle = 30.0F;
	private string lectura;
	private int sensor0=0, sensor1=0;
	private float valSens0,valSens1;

	SerialPort sp = new SerialPort("COM5",9600); // cambiar el puerto.

	// Use this for initialization
	void Start () {
		sp.Open ();
		sp.ReadTimeout = 1;

	}
	
	// Update is called once per frame
	void Update () {
		if (sp.IsOpen) {
			try {
				lectura = sp.ReadLine();
				Debug.Log(lectura);

			} 
			catch (System.Exception) {
				lectura = "0 ; 0";
			}
		} else {
			Debug.Log("esta cerrado");
		}



		try{
		sensor0 = Int32.Parse(lectura.Substring(0,lectura.IndexOf(" ;")));
			Debug.Log (sensor0);
		sensor1 = Int32.Parse(lectura.Substring(lectura.IndexOf("; ")+2, lectura.Length - lectura.IndexOf("; ")-2));
			Debug.Log (sensor1);
		}
		catch
		{
			sensor0 = 0;
			sensor1=0;
		}

		//Debug.Log(sensor0.ToString() + ";" + sensor1.ToString());
		//transform.Rotate((new Vector3 (sensor0*1.0f,0.0f, 0.0f)) * tiltAngle);

		if (Math.Abs (sensor0) > 28) {
			transform.Rotate ((new Vector3 (sensor0*1.0f, 0.0f, 0.0f)) * tiltAngle);


		} else {
			if (Math.Abs (sensor1) > 28) {
				transform.Rotate ((new Vector3 (0.0f, sensor1*1.0f, 0.0f)) * tiltAngle);
			} else {
				transform.Rotate (new Vector3 (0.0f, 0.0f, 0.0f));
			}
		}

	}
}


