using UnityEngine;
using System.Collections;

public class PanControlls : MonoBehaviour {


	public float panSpeed;
	public float rotSpeed;

	// Use this for initialization
	void Start () {

		rotSpeed = GameObject.Find ("Main Camera").GetComponent<CameraScript> ().rotSpeed;
	
	}
	

	void Update () {

//		Vector3 w = new Vector3 (1,0,0);
//		Vector3 s = new Vector3 (-1,0,0);
//		Vector3 a = new Vector3 (0,0,-1);
//		Vector3 d = new Vector3 (0,0,1);
		
		Vector3 cPos = transform.position;
		Quaternion cRot = transform.rotation;
//		Vector3 cTempRot = cRot.eulerAngles;
		
//		float cPosX = cPos.x;
//		float cPosZ = cPos.z;
//
//		cPos.y = 0;

//		cPos += Vector3.right * cPosX - Vector3.right * cPos.x;
//		cPos += Vector3.forward * cPosZ - Vector3.forward * cPos.z;

		//-------
	
		if (Input.GetKey (KeyCode.W)) {
			cPos += transform.forward * Time.deltaTime * panSpeed;
		}
		if (Input.GetKey (KeyCode.S)) {
			cPos += -transform.forward * Time.deltaTime * panSpeed;
		}
		if (Input.GetKey (KeyCode.A)) {
			cPos += -transform.right * Time.deltaTime * panSpeed;
		}
		if (Input.GetKey (KeyCode.D)) {
			cPos += transform.right * Time.deltaTime * panSpeed;
		}
//		if (Input.GetKey(KeyCode.Q)) {
//			cTempRot += Time.deltaTime * rotSpeed * cTempRot;
//		}
//		if (Input.GetKey(KeyCode.E)) {
//			cTempRot += Time.deltaTime * rotSpeed * cTempRot;
//		}

		if (cPos.x < -25f) {
			cPos.x = -25f;
		}
		if (cPos.x > 25f) {
			cPos.x = 25f;
		}
		if (cPos.z < -25f) {
			cPos.z = -25f;
		}
		if (cPos.z > 25f) {
			cPos.z = 25f;
		}

//		cRot = cTempRot;
	
		transform.position = cPos;
		transform.rotation = cRot;
	}
}
