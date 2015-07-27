using UnityEngine;
using System.Collections;

public class Resourse : MonoBehaviour {

	public float nutrients;
	public float water;
	public float materialInUse;
	public int childCount;

//	public float resTimer;
//	public float nutrientUsage;
//	public float waterUsage = 20;

//	public bool plantClose;
//	public bool waterUsed;
//	public bool waterSetBack;

	public Color hexColor;

//	public GameObject[] plant;
//	public GameObject plantTest;
//	public Quaternion plantPos;

	void Start () {

		childCount = gameObject.transform.childCount;

		//Sets random amount of recourses to the hex
		nutrients = Random.Range (0, 101);
		water = Random.Range (0, 101);

		//Colors the hexes based on how much recourses it has (water = blue, nutrients = green)
		hexColor = new Color (0, nutrients / 100 , water / 100 , 1);
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);

	}

	void Update () {

//		if (Input.GetKeyDown (KeyCode.Space)) {
//			Instantiate (plantTest, transform.position, plantPos);
//		}

		//If there is a plant, put it in the array
//		if (plant != null) {
//		plant = GameObject.FindGameObjectsWithTag ("Plant");
//		}
//		if (plant == null) {
//			plantClose = false;
//		}


		//Is there a plant close by?
//		for (int i = 0; i < plant.Length; i++) {
//			if (Vector3.Distance (plant[i].transform.position , gameObject.transform.position) < 2f) {
//				plantClose = true;
//			} else {
//				plantClose = false;
//			}
//		}

//		if (gameObject.GetComponent<Transform> ().FindChild ("Plant(Clone)") == true) {
//			plantClose = true;
//		}
//		if (gameObject.GetComponent<Transform> ().FindChild ("Plant(Clone)") == false) {
//			plantClose = false;
//		}

		//Water used if there is a plant close by
//		if (plantClose == true && waterUsed == false) {
//			water -= waterUsage;
//			nutrients -= nutrientUsage * Time.deltaTime;
//			waterUsed = true;
//			waterSetBack = false;
//		}
//
//		//Releases the used water if there is no plant nearby
//		if (plantClose == false && waterSetBack == false) {
//			water += waterUsage;
//			waterUsed = false;
//			waterSetBack = true;
//		}

		//Checks for the material that is being used at the moment
		materialInUse = GameObject.Find ("GM").GetComponent<MouseScript> ().materialInUse;

		//Colors the hexes based on how much recourses it has (water = blue, nutrients = green)
		if (materialInUse > 7) {
			hexColor = new Color (0, nutrients / 100 , water / 100 , 1);
			gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);
		}

		//Caps the recourses between 0 and 100
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
