using UnityEngine;
using System.Collections;

public class kasviTesti : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.A)) {
			anim.SetBool ("hasNutrients", true);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			anim.SetBool ("canFlower", true);
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			anim.SetBool ("canFlower", false);
		}
	}
}
