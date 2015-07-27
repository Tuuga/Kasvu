using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	public GameObject parentHex;
	public float nutrientUse = 10;

	void Start () {
		parentHex = transform.parent.gameObject;
		parentHex.GetComponent<Resourse> ().water -= 20;
	}

	void Update () {

		if (parentHex.GetComponent<Resourse> ().nutrients > 0) {
			parentHex.GetComponent<Resourse> ().nutrients -= nutrientUse * Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Destroy (gameObject);
		}
	}

	void OnDestroy () {
		parentHex.GetComponent<Resourse> ().water += 20;
	}
}
