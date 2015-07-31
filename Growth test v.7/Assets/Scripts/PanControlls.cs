using UnityEngine;
using System.Collections;

public class PanControlls : MonoBehaviour {

	float posY;

	void Start () {

		posY = transform.position.y;
	}

	void Update () {

		Vector3 cPos = transform.position;
		Quaternion cRot = transform.rotation;

		cPos.y = posY;
	
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

		transform.position = cPos;
		transform.rotation = cRot;
	}
}
