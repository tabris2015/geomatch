using UnityEngine;
using System.Collections;

public class punto : MonoBehaviour {
	public TextMesh texto;
	public string label = "test";
	// Use this for initialization
	void Start () {
		texto.text = label;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
