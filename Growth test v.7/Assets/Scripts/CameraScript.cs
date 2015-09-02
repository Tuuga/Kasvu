using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Color bgColor;
	GameObject dirLight;
	public Transform center;

	public Quaternion startRot;

	public float scrollSpeed;

	public Vector3 pos;

	public Vector2 scrollU;
	public Vector2 scrollD;

	void Start () {
		dirLight = GameObject.Find ("Directional Light");
	}

	void Update () {

		scrollU = new Vector2 (0,1);
		scrollD = new Vector2 (0,-1);

		pos = transform.position;

		float scrollVal = Vector3.Distance (center.position, pos);

		//Zoom by scrolling
		if (Input.mouseScrollDelta == scrollU) {
			pos += transform.forward * scrollVal * scrollSpeed;
		}
		if (Input.mouseScrollDelta == scrollD) {
			pos -= transform.forward * scrollVal * scrollSpeed;
		}

		bgColor = dirLight.GetComponent<Light>().color;

		GetComponent<Camera>().backgroundColor = bgColor;

		transform.position = pos;
	}

	void LateUpdate () {

		//camera focus on point
		transform.LookAt (center);

	}
}
