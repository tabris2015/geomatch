using UnityEngine;
using System.Collections;

public class matched : MonoBehaviour {

	// Use this for initialization
	public GameObject chispa;
	GameObject chispa_;
	void Start () {

		NotificationCenter.DefaultCenter().AddObserver(this, "match");
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void match(Notification noti)
	{
		int id = (int)noti.data;
		if (gameObject.name == id.ToString() && chispa != null)
		{
			chispa_ = Instantiate(chispa, transform.position, Quaternion.identity) as GameObject;
			chispa_.transform.parent = transform;
		}
	}
}
