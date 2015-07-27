using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour {

	public float fps;
	Text text;

	void Awake () {
		text = GetComponent <Text> ();
	}

	void Update () {

		fps += (Time.deltaTime - fps) * 0.1f;

		float newFPS = 1.0f / fps;
	
		text.text = "FPS: " + newFPS;
	}
}
