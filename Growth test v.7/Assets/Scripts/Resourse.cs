using UnityEngine;
using System.Collections;

public class Resourse : MonoBehaviour {

	public float nutrients;
	public float water;
	public float materialInUse;
	public int childCount;

	// Heks position in grid in axis cordinates.
	public int xPos;
	public int yPos;

//	public float resTimer;
//	public float nutrientUsage;
//	public float waterUsage = 20;

//	public bool plantClose;
//	public bool waterUsed;
//	public bool waterSetBack;

	public Color hexColor;
	public Material test;

//	public GameObject[] plant;
//	public GameObject plantTest;
//	public Quaternion plantPos;

	bool w;
	bool n;

	void Start () {

		childCount = gameObject.transform.childCount;

		//Sets random amount of recourses to the hex
		nutrients = Random.Range (0, 101);
		water = Random.Range (0, 101);

//		nutrients = 100;
//		water = 100;

		//Colors the hexes based on how much recourses it has (water = blue, nutrients = green)
		hexColor = new Color (0, 0 , water / 100 , nutrients / 100);
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);

	}

	void Update () {

		if (water > Random.Range (70, 100)) {
			w = true;
		}
		if (water < Random.Range (0, 30)) {
			w = false;
		}
		if (nutrients > Random.Range (70, 100)) {
			n = true;
		}
		if (nutrients < Random.Range (0, 30)) {
			n = false;
		}

//		if (w == false) {
//			water += 100 * Time.deltaTime;
//		}
//		if (w == true) {
//			water -= 100 * Time.deltaTime;
//		}
//
//		if (n == false) {
//			nutrients += 100 * Time.deltaTime;
//		}
//		if (n == true) {
//			nutrients -= 100 * Time.deltaTime;
//		}



		//Checks for the material that is being used at the moment
		materialInUse = GameObject.Find ("GM").GetComponent<MouseScript> ().materialInUse;

		//Colors the hexes based on how much recourses it has (water = blue, nutrients = green)
		if (materialInUse > 7) {
			hexColor = new Color (0, 0 , water, nutrients) * 0.01f;
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
