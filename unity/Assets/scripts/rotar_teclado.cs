using UnityEngine;
using System.Collections;

public class rotar_teclado : MonoBehaviour {
	public float smooth = 2.0F;
	public float tiltAngle = 30.0F;

	void Update() {
		float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
		float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
		Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundZ, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
	}
}