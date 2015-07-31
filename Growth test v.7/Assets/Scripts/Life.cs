using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

	public GameObject life;
	GameObject parentHex;

	int radius;

//	void Awake () {
//		parentHex = gameObject.transform.parent;
//
//		int X = parentHex.GetComponent<Resourse> ().xPos;
//		int Y = parentHex.GetComponent<Resourse> ().yPos;
//		int R = radius;
//		for(int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
//			for(int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
//				Hexes[x + y * key].GetComponent<Renderer> ().material = colors [materialInUse];
//			}
//		}
//	}

	void Update () {
	
	}
}
