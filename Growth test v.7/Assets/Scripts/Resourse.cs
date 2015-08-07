﻿using UnityEngine;
using System.Collections;

public class Resourse : MonoBehaviour {
	
	public float waterMin;
	public float waterMax;
	public float nutrientMin;
	public float nutrientMax;

	public float nutrients;
	public float water;
	public bool drawMode;
	public int childCount;

	// Hex position in grid in axis cordinates.
	public int xPos;
	public int yPos;

	public Color hexColor;

	public bool lifeCanGrow;
	public bool lifeSpawned;
	public GameObject lifePrefab;
	GameObject life;
	Quaternion zeroRot;

	void Start () {

		zeroRot = new Quaternion (0, 0, 0, 0);

		childCount = gameObject.transform.childCount;

		//Sets random amount of recourses to the hex
		nutrients = Random.Range (nutrientMin, nutrientMax);
		water = Random.Range (waterMin, waterMax);

		//Colors the hexes based on how much recourses it has (water = blue, nutrients = green)
		hexColor = new Color (0, nutrients / 100 , water / 100 , 1);
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", hexColor);

	}

	void Update () {

		if (lifeCanGrow == true && lifeSpawned == false) {
			life = (GameObject)Instantiate (lifePrefab, transform.position, zeroRot);
			life.transform.parent = gameObject.transform;
			lifeSpawned = true;
		}
		if (lifeCanGrow == false && life != null) {
			Destroy (life);
			lifeSpawned = false;
		}

		//Checks for the material that is being used at the moment
		drawMode = GameObject.Find ("GM").GetComponent<MouseScript> ().drawMode;

		//Colors the hexes based on how much recourses it has (water = blue, nutrients = green)
		if (drawMode == false) {
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
