using UnityEngine;
using System.Collections;

public class particle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W)) {
			gameObject.GetComponent<ParticleSystem> ().Emit(100);
//			gameObject.GetComponent<ParticleSystem> ().star
		}
	}
}
