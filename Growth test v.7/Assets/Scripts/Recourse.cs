using UnityEngine;
using System.Collections;

public class Recourse : MonoBehaviour {

	public float nutrients;
	public float water;
	public float materialInUse;

	public Color hexColor;

	void Start () {

		nutrients = Random.Range (0, 101);
		water = Random.Range (0, 101);

		hexColor = new Color (0, 0, water / 100 , nutrients / 100);
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);


	}

	void Update () {

		materialInUse = GameObject.Find ("GM").GetComponent<MouseScript> ().materialInUse;

		if (materialInUse > 7) {
			hexColor = new Color (0, 0, water / 100 , nutrients / 100);
			gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);
		}

		if (nutrients > 100) {
			nutrients = 100;
		}
		if (nutrients < 0) {
			nutrients = 0;
		}
		if (water > 100) {
			water = 100;
		}
		if (water < 0) {
			water = 0;
		}
	}
}
