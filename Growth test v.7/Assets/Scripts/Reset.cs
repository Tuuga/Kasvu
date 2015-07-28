using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	public Material white;

	void Update () {

		if (Input.GetKeyDown(KeyCode.R)) {
			gameObject.GetComponent<Renderer>().material = white;
		}
	}
}
