using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugUI : MonoBehaviour {

	GameObject mainCamera;
	public float speed;

	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
	}

	void Update () {

		float step = speed * Time.deltaTime;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, mainCamera.transform.rotation, step);
	}
}
