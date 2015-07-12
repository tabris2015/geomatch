using UnityEngine;
using System.Collections;

public class rayo : MonoBehaviour {

	public int distancia_rayo = 15;
	public int id;
	public string nivel_str;
	// Use this for initialization
	void Start () {
	
	}
	

	// Update is called once per frame
	void Update () {
		Ray rayo = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(rayo, out hitInfo, distancia_rayo))
		{
			string tagObjeto = hitInfo.collider.gameObject.tag;
			string nombreObjeto = hitInfo.collider.gameObject.name;

			Debug.DrawRay(transform.position, transform.forward);

				if(tagObjeto == nivel_str)
				{
					Debug.Log("nivel_match");
					

					if(nombreObjeto == id.ToString())
					{
						Debug.Log("casilla_match");
						NotificationCenter.DefaultCenter().PostNotification(this, "match", id);
						Destroy(this.gameObject);
					}

				}

		}
	}
}
