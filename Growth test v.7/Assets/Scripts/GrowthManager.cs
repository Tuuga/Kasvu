using UnityEngine;
using System.Collections;

public class GrowthManager : MonoBehaviour {

	public GameObject lifePrefab;
	GameObject parentHex;
	GameObject hexInUse;
	
	GameObject[] Hexes;

	Quaternion zero;
	
	Grid axisGrid;

	int key;
	int X;
	int Y;
	int R;

	void Start () {

		axisGrid = gameObject.GetComponent<Grid> ();
		Hexes = axisGrid.heksagons;
		key = axisGrid.gridWidthInHexes + (axisGrid.gridHeightInHexes - 1) / 2;
	}

	public void SetLifeInHex (GameObject plant) {
		
		parentHex = plant.transform.parent.gameObject;
		R = plant.GetComponent<PlantLife> ().maxLifeRadius;
		X = parentHex.GetComponent<Resourse> ().xPos;
		Y = parentHex.GetComponent<Resourse> ().yPos;
		for (int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
			for (int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
				Hexes [x + y * key].GetComponent<Resourse> ().lifeInHex ++;
			}
		}
	}

	public void SpawnLife (GameObject plant) {

		parentHex = plant.transform.parent.gameObject;
		R = plant.GetComponent<PlantLife> ().lifeRadius;
		X = parentHex.GetComponent<Resourse> ().xPos;
		Y = parentHex.GetComponent<Resourse> ().yPos;

		for (int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
			for (int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
				hexInUse = Hexes [x + y * key];

				if (hexInUse.GetComponent<Resourse> ().lifeInHex > 0 && !hexInUse.transform.FindChild ("Life")) {
					GameObject lifeIns = (GameObject)Instantiate (lifePrefab, hexInUse.transform.position, new Quaternion (0, 0, 0, 0));
					lifeIns.name = "Life";
					lifeIns.transform.LookAt (parentHex.transform);
					lifeIns.transform.parent = hexInUse.transform;

				}
			}
		}
	}

	public void DestroyLife (GameObject plant) {
			
		parentHex = plant.transform.parent.gameObject;
		X = parentHex.GetComponent<Resourse> ().xPos;
		Y = parentHex.GetComponent<Resourse> ().yPos;
		R = plant.GetComponent<PlantLife> ().maxLifeRadius;

		for (int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
			for (int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
				hexInUse = Hexes [x + y * key];

				hexInUse.GetComponent<Resourse> ().lifeInHex --;
				if (hexInUse.transform.Find ("Life") != null && hexInUse.GetComponent<Resourse> ().lifeInHex < 1) {
					Destroy (hexInUse.transform.Find ("Life").gameObject);

				}
			}
		}
	}
}
