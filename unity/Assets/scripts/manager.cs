using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//NotificationCenter.DefaultCenter().AddObserver(this, "Comenzar");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Comenzar()
	{
		Application.LoadLevel(1);
	}
}
