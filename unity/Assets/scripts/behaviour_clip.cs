using UnityEngine;
using System.Collections;

public class behaviour_clip : MonoBehaviour {

	// Use this for initialization
	public AudioClip score;
	public AudioClip levelup;
	public AudioClip finish;
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "match");
		NotificationCenter.DefaultCenter().AddObserver(this, "level_clear");
		NotificationCenter.DefaultCenter().AddObserver(this, "victory");
		audio.loop = false;


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void match(Notification noti)
	{
		audio.Stop ();
		audio.clip = score;
		audio.Play();
		Debug.Log("punto!");

	}

	void level_clear(Notification noti)
	{
		audio.Stop ();
		audio.clip = levelup;
		audio.Play();
	}
	void victory(Notification noti)
	{
		audio.Stop ();
		audio.clip = finish;
		audio.Play();
	}



}
