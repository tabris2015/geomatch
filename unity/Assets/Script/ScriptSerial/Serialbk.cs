using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;


	public class Serialbk : MonoBehaviour {

	public float vel = 10;
	private string lectura;
	private int sensor0=0, sensor1=0;
	private float yaw_s, pitch_s, roll_s;
	public float yaw, pitch, roll;
	public double heading=0;
	public double direccion;
	private float yaw_a;
	private float pitch_a;
	private float roll_a;
	private float yaw_anterior =0.0f;
	public float sens = 0;
	public float bitch = 0.0f;
	public float rotScale=5.0f;
	private bool primera_flag = true;
	private float rotVertical=0.0f,rotHorizontal=0.0f;

	private float valSens0,valSens1;

	SerialPort sp = new SerialPort("COM5",115200); // cambiar el puerto.

	// Use this for initialization
	void Start () {
		try{
		sp.Open ();
		sp.ReadTimeout = 2;
		}
		catch
		{
			Debug.Log ("Fallo el puerto serial");
		}
		yaw_a = 0;
		pitch_a = 0;
		roll_a = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Quaternion AddRot = Quaternion.identity;
		if (sp.IsOpen) {
			try {

				lectura = sp.ReadLine();
				//Debug.Log(lectura);

			} 
			catch (System.Exception) {
				//lectura = "0 ; 0 ; 0 ; 0 ; 0 ; 0";
				print ("no hay lectura en el puerto");
			}
				
			try{
				char [] separador = {';'};
				string [] split = lectura.Split(separador);
				//print (split);
				yaw_s = float.Parse(split[0]);
				pitch_s =float.Parse(split[1]);
				roll_s =float.Parse(split[2]);
				heading = Math.Atan2(double.Parse(split[4]),double.Parse(split[3]))*180.0f/Math.PI;// - Convert.ToDouble(yaw_s);
				
				direccion = heading;
			}
			
			catch
			{
				Debug.Log("no hay lectura");
				
			}
			
			if(Math.Abs(yaw_a-yaw_s) > sens)	yaw = yaw_s;
			else yaw = yaw_a;
			if(Math.Abs(pitch_a-pitch_s) > sens*1.5f) 	pitch = pitch_s;
			else pitch = pitch_a;
			if(Math.Abs(roll_a-roll_s) > sens*2.0f) 	roll = roll_s;
			else roll = roll_a;

			AddRot.eulerAngles = new Vector3(-pitch, (float)(heading), -roll);
		} else {
			Debug.Log("esta cerrado");
			//Quaternion AddRot = Quaternion.identity;
			if(Input.GetAxis("Vertical")>0){
				rotVertical+=1;
			}
			else{
				if(Input.GetAxis ("Vertical")<0){
					rotVertical-=1;
				}
			}
			if(Input.GetAxis("Horizontal")>0){
				rotHorizontal+=1;
			}
			else{
				if(Input.GetAxis ("Horizontal")<0){
					rotHorizontal-=1;
				}
			}
			
			AddRot.eulerAngles = new Vector3(-rotVertical*rotScale, rotHorizontal*rotScale, -roll);

		}


		rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, AddRot, Time.time*vel);
		yaw_a = yaw_s;
		pitch_a = pitch_s;
		roll_a = roll_s;
		yaw_s = 0;
		pitch_s = 0; 
		roll_s = 0;
	
	}
}