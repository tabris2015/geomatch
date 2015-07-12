using UnityEngine;
using System.Collections;

public class raycastmio : MonoBehaviour {
	float distance = 50.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetMouseButtonDown(0))
		{
			Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if(Physics.Raycast(rayOrigin, out hitInfo, distance))
			{
				Debug.Log("lanzando un rayo");
				Debug.DrawLine(rayOrigin.direction, hitInfo.point);
			}
		}

	
	}
}
