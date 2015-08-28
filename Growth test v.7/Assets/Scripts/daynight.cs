using UnityEngine;
using System.Collections;

public class daynight : MonoBehaviour {

	public float rotSpeed;

	void Update () {
	
		float rotX = transform.rotation.x;
		rotX += rotSpeed * Time.deltaTime;

		transform.rotation = new Quaternion (rotX,0,0,0);
	}
}
