using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {
	
	public int radius = 1;
	int startR;

//	public GameObject life;
	GameObject parentHex;
	GameObject[] Hexes;
	int key;
	Grid axisGrid;

	int X;
	int Y;
	int R;

	void Start () {

		Debug.Log ("LIFE");
		axisGrid = GameObject.Find ("GM").GetComponent<Grid> ();
		Hexes = axisGrid.heksagons;
		key = axisGrid.gridWidthInHexes + (axisGrid.gridHeightInHexes - 1) / 2;

		parentHex = gameObject.transform.parent.gameObject;

		X = parentHex.GetComponent<Resourse> ().xPos;
		Y = parentHex.GetComponent<Resourse> ().yPos;
		R = radius;
		//Radius for growing
		for(int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
			for(int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
				Hexes[x + y * key].GetComponent<Resourse>().lifeCanGrow = true;
			}
		}
	}

	void Update () {

		//Checks if the radius has changed
		//Resets the Life Can Grow bool based on the new radius
		if (radius - R != 0) {
			for(int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
				for(int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
					Hexes[x + y * key].GetComponent<Resourse>().lifeCanGrow = false;
				}
			}
			R = radius;
			for(int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
				for(int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
					Hexes[x + y * key].GetComponent<Resourse>().lifeCanGrow = true;
				}
			}
		}
		// inputit radiuksen vaihtoon
		if (Input.GetKeyDown (KeyCode.C)) {
			radius --;
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			radius ++;
		}
	}
}
