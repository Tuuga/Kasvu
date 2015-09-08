using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	void Update () {

		if (Input.GetKey (KeyCode.Space) && Input.GetKey (KeyCode.LeftShift)) {
			Application.LoadLevel (0);
		}
	}
}
