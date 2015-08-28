using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour {

	float fps;
	Text text;

	void Awake () {
		text = GetComponent <Text> ();
	}

	void Update () {

		fps += (Time.deltaTime - fps) * 0.1f;
		float newFPS = 1.0f / fps;
		newFPS = Mathf.Round (newFPS);

		if (newFPS > 50) {
			text.color = Color.green;
		}
		if(newFPS < 50 && newFPS > 30){
			text.color = Color.yellow;
		}
		if (newFPS < 30) {
			text.color = Color.red;
		}
		text.text = "FPS: " + newFPS;
	}
}
