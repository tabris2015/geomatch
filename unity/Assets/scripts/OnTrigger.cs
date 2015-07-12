using UnityEngine;
using System.Collections;

public class OnTrigger : MonoBehaviour {

	public ParticleSystem particle;

	void Start() {
		particle.Stop();
		//Random rnd = new Random();
		//int fichas = rnd.
		Debug.Log(Random.Range(1, 12));
	}

	//bug.LogType("Entrada");
    void OnTriggerEnter (Collider other) {
		Debug.Log ("Adentro");
		if (!particle.isPlaying) {
			particle.Play();	
		}
        //Destroy(this.gameObject);
    }


}
