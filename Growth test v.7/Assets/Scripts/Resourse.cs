using UnityEngine;
using System.Collections;

public class Resourse : MonoBehaviour {

	public float lifeInHex;

	public float waterMin;
	public float waterMax;
	public float nutrientMin;
	public float nutrientMax;

	public float nutrients;
	public float water;
	public bool drawMode;
	public int childCount;

	// Hex position in grid in axis cordinates.
	[HideInInspector]
	public int xPos;
	[HideInInspector]
	public int yPos;

	[HideInInspector]
	public Color hexColor;

	public bool lifeCanGrow;
	public bool lifeSpawned;

	public GameObject lifePrefab;

	void Awake () {

		childCount = gameObject.transform.childCount;

		//Sets random amount of recourses to the hex
//		nutrients = Random.Range (nutrientMin, nutrientMax);
//		water = Random.Range (waterMin, waterMax);

	}

	void Update () {

		//Caps the recourses between 0 and 100
		nutrients = Mathf.Clamp (nutrients, 0, 100);
		water = Mathf.Clamp (water, 0, 100);

	}
}
