using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResUI : MonoBehaviour {

	GameObject parentHex;

	public float timeToUpdate;
	float timer;

	float water;
	float nutrients;

	Text text;

	string waterColor;
	string nutrientColor;

	void Start () {
		text = GetComponent <Text> ();
		waterColor = GameObject.Find ("UI Manager").GetComponent<UIManagerScript>().waterColorRGBA;
		nutrientColor = GameObject.Find ("UI Manager").GetComponent<UIManagerScript>().nutrientColorRGBA;
		parentHex = gameObject.transform.parent.parent.parent.gameObject;
		UIUpdate ();
	}

	void Update () {

		timer += Time.deltaTime;
		if (timer > timeToUpdate) {
			timer -= timeToUpdate;
			UIUpdate();
		}
	}

	void UIUpdate () {

		water = Mathf.Round (parentHex.GetComponent<Resourse>().water);
		nutrients = Mathf.Round (parentHex.GetComponent<Resourse>().nutrients);
		text.text = "<color="+waterColor+">W: " + water + "</color>" + "\n<color="+nutrientColor+">N: " + nutrients + "</color>";
	}
}
