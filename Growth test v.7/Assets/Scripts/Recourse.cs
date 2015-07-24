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

		hexColor = new Color (0, nutrients / 100 , water / 100 , 1);
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);


	}

	void Update () {

		materialInUse = GameObject.Find ("GM").GetComponent<MouseScript> ().materialInUse;

		if (materialInUse > 7) {
			hexColor = new Color (0, nutrients / 100 , water / 100 , 1);
			gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);
		}

		if (nutrients > 99) {
			nutrients = 100;
		}
		if (nutrients < 1) {
			nutrients = 0;
		}
		if (water > 99) {
			water = 100;
		}
		if (water < 1) {
			water = 0;
		}
	}
}
