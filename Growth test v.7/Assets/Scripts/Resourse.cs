using UnityEngine;
using System.Collections;

public class Resourse : MonoBehaviour {

	public float nutrients;
	public float water;
	public float materialInUse;
	public int childCount;

	// Hex position in grid in axis cordinates.
	public int xPos;
	public int yPos;

	public Color hexColor;
	public Material test;

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

		//Checks for the material that is being used at the moment
		materialInUse = GameObject.Find ("GM").GetComponent<MouseScript> ().materialInUse;

		//Colors the hexes based on how much recourses it has (water = blue, nutrients = green)
		if (materialInUse > 7) {
			hexColor = new Color (0, nutrients / 100 , water / 100, 1);
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
