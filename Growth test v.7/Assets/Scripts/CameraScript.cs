using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform center;

	public Quaternion startRot;

	public float rotSpeed;
	public float panSpeed;
	public float scrollSpeed;

	public Vector3 pos;

	public Vector2 scrollU;
	public Vector2 scrollD;

	void Update () {

//		Vector3 w = new Vector3 (1,0,0);
//		Vector3 s = new Vector3 (-1,0,0);
//		Vector3 a = new Vector3 (0,0,-1);
//		Vector3 d = new Vector3 (0,0,1);
//
//		Vector3 cPos = center.transform.position;
//
//		float cPosX = cPos.x;
//		float cPosZ = cPos.z;
//
//		cPos.y = 0;
//
//		// Clamp won't work
//		float cXMin = -25;
//		float cXMax = 25;
//		float cZMin = -15;
//		float cZMax = 25;
//
//		Mathf.Clamp (cPosX, cXMin, cXMax);
//		Mathf.Clamp (cPosZ, cZMin, cZMax);
//
//		cPos.x = cPosX;
//		cPos.z = cPosZ;

		scrollU = new Vector2 (0,1);
		scrollD = new Vector2 (0,-1);

		pos = transform.position;

		float scrollVal = Vector3.Distance (center.position, pos);

//		Debug.Log (scrollVal);

		//E and Q rotates camera around the focuse point
		if (Input.GetKey(KeyCode.Q)) {
			pos += -transform.right * Time.deltaTime * rotSpeed;
		}
		if (Input.GetKey(KeyCode.E)) {
			pos += transform.right * Time.deltaTime * rotSpeed;
		}
		//Zoom by scrolling
		if (Input.mouseScrollDelta == scrollU) {
			pos += transform.forward * scrollVal * scrollSpeed;
		}
		if (Input.mouseScrollDelta == scrollD) {
			pos -= transform.forward * scrollVal * scrollSpeed;
		}


		//Camera pan - BROKEN

//		if (Input.GetKey (KeyCode.W)) {
//			cPos += transform.forward * Time.deltaTime * panSpeed;
//			pos += w * Time.deltaTime * panSpeed;
//		}
//		if (Input.GetKey (KeyCode.S)) {
//			cPos += -transform.forward * Time.deltaTime * panSpeed;
//			pos += s * Time.deltaTime * panSpeed;
//		}
//		if (Input.GetKey (KeyCode.A)) {
//			cPos += -transform.right * Time.deltaTime * panSpeed;
//			pos += a * Time.deltaTime * panSpeed;
//		}
//		if (Input.GetKey (KeyCode.D)) {
//			cPos += transform.right * Time.deltaTime * panSpeed;
//			pos += d * Time.deltaTime * panSpeed;
//		}
//
//		center.transform.position = cPos;
		transform.position = pos;
	}

	void LateUpdate () {

		//camera focus on point

		transform.LookAt (center);
//		Vector3 pos = transform.position;
//		pos.y = 20;
//		transform.position = pos;

	}
}
