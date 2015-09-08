using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	float resHexCount;
//	float waterUse = 0;
//	float nutrientUse = 0;

	float sharedWaterUse;
	float sharedNutrientUse;

//	float totalWaterInRadius;
//	float totalNutrientInRadius;

	public int lifeStartRadius;

	float growTimer;
	int growCurrentLevel;
	public int growMaxLevel;
	float growTime;
	public float growStep;
	bool grew;

//	int resRadius = 0;

	
	GameObject parentHex;
	GameObject[] Hexes;
	GameObject[] hexesInRadius;
	int key;
	Grid axisGrid;

	int X;
	int Y;
	int R;

	void Start () {

		growTime = growStep;

		axisGrid = GameObject.Find ("GM").GetComponent<Grid> ();
		Hexes = axisGrid.heksagons;
		key = axisGrid.gridWidthInHexes + (axisGrid.gridHeightInHexes - 1) / 2;

		parentHex = gameObject.transform.parent.gameObject;

		X = parentHex.GetComponent<Resourse> ().xPos;
		Y = parentHex.GetComponent<Resourse> ().yPos;
	}

	void Update () {

		//Time for growth
		growTimer += Time.deltaTime;
		if (growTimer > growTime && growCurrentLevel < growMaxLevel) {

			lifeStartRadius ++;
			growCurrentLevel ++;
			growTime += growStep;
		}

		//Checks if the radius has changed
		//Resets the Life Can Grow bool based on the new radius
		if (lifeStartRadius - R != 0) {
			R = lifeStartRadius;
			for(int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
				for(int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
					Hexes[x + y * key].GetComponent<Resourse>().lifeCanGrow = false;
				}
			}
		}

		R = lifeStartRadius;
		for(int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
			for(int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
				Hexes[x + y * key].GetComponent<Resourse>().lifeCanGrow = true;
			}
		}
		if (lifeStartRadius < 0) {
			lifeStartRadius = 0;
		}
	}
}
