using UnityEngine;
using System.Collections;

public class Recourse : MonoBehaviour {

	public float nutrients;
	public float water;
	public float materialInUse;
	public float resTimer;
	public float resUsage = 50;

	public bool plantClose;

	public Color hexColor;

	public GameObject[] plant;

	void Start () {

		nutrients = Random.Range (0, 101);
		water = Random.Range (0, 101);

		hexColor = new Color (0, nutrients / 100 , water / 100 , 1);
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);


	}

	void Update () {

		if (plant != null) {
		plant = GameObject.FindGameObjectsWithTag ("Plant");
		}
		if (plant == null) {
			plantClose = false;
		}

		for (int i = 0; i < plant.Length; i++) {
			if (Vector3.Distance (plant[i].transform.position , gameObject.transform.position) < 2f) {
				plantClose = true;
			} else {
				plantClose = false;
			}
		}
		if (plantClose == true) {
			if (water > 99) {
				water --;
			}
			water -= resUsage * Time.deltaTime;
		}

		if (plantClose == false) {
			water += resUsage * Time.deltaTime;
		}

//		Debug.Log (plant.Length);

		materialInUse = GameObject.Find ("GM").GetComponent<MouseScript> ().materialInUse;

		if (materialInUse > 7) {
			hexColor = new Color (0, nutrients / 100 , water / 100 , 1);
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
