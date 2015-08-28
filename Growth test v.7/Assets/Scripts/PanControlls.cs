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

		cPos.x = Mathf.Clamp (cPos.x, -15, 15);
		cPos.z = Mathf.Clamp (cPos.z, -9, 9);
		cPos.y = posY;
	
		transform.position = cPos;
		transform.rotation = cRot;
	}
}
