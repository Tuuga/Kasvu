using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HF : MonoBehaviour {

	public static GameObject[] Hexes;
	public static int key;
	public static int gridHeight;
	public static int gridWidth;

	// Gives index to use based on cordinates.
	static public int CorToInd (int X, int Y) {
		return X + Y * key;
	}

	// Gives cordinates to use based on index.
	static public int[] IndToCor (int I) {
		int[] cordinatesXY = new int[2] {I % key, I / key};
		return cordinatesXY;
	}

	// Start of a hex radius Y loop.
	static public int YStart (int R, int Y) {
		return Mathf.Max (Y - R, 0);
	}
	
	// End of a hex radius Y loop.
	static public int YEnd (int R, int Y) {
		return Mathf.Min (Y + R, gridHeight - 1);
	}
	
	// Start of a hex radius X loop.
	static public int XStart (int R, int X, int Y, int y) {
		return Mathf.Max(X - R, X - R + y - Y, 0 + y / 2);
	}
	
	// End of a hex radius X loop.
	static public int XEnd (int R, int X, int Y, int y) {
		return Mathf.Min(X + R, X + R + y - Y, gridWidth + y / 2 - 1);
	}

	// Distance of a set of given cordinates to another set of given cordinates.
	static public int PosInRad (int X, int Y, int x, int y) {
		int Distance;
		if (x - X != 0 && y - Y != 0 && ((x - X) / Mathf.Abs(x - X)) + ((y - Y) / Mathf.Abs(y - Y)) == 0) { // Mathf.Abs((x - X) - (y - Y)) > Mathf.Max(Mathf.Abs(x - X), Mathf.Abs(y - Y))
			Distance = Mathf.Abs((x - X) - (y - Y));
		} else {
			Distance = Mathf.Max(Mathf.Abs(x - X), Mathf.Abs(y - Y));
		}
		return Distance;
	}

	// List of hexes within a certain radius from certain cordinates.
	static public List <GameObject> HexesList (int R, int X, int Y) {
		List <GameObject> hexesList = new List<GameObject> ();
		int y = YStart (R, Y);
		int yCap = YEnd (R, Y);
		for(; y <= yCap; y ++) {
			int x = XStart (R, X, Y, y);
			int xCap = XEnd (R, X, Y, y);
			for(; x <= xCap; x ++) {
				hexesList.Add(Hexes[CorToInd (x, y)]);
			}
		}
		return hexesList;
	}

	// List of hexes within a certain radius from certain cordinates set to sublists acording to their distance from center.
	static public List <List <GameObject>> HexesListRad (int R, int X, int Y) {
		List <List <GameObject>> hexesList = new List<List <GameObject>> ();
		int y = YStart (R, Y);
		int yCap = YEnd (R, Y);
		for(; y <= yCap; y ++) {
			int x = XStart (R, X, Y, y);
			int xCap = XEnd (R, X, Y, y);
			for(; x <= xCap; x ++) {
				int Distance = PosInRad (X, Y, x, y);
				while(hexesList.Count < Distance + 1) {
					hexesList.Add(new List<GameObject> ());
				}
				hexesList[Distance].Add(Hexes[CorToInd (x, y)]);
			}
		}
		return hexesList;
	}

	// Array of hexes within a certain radius from certain cordinates.
	static public GameObject[] HexesArray (int R, int X, int Y) {
		List <GameObject> hexesList = HexesList(R, X, Y);
		GameObject[] hexesArray = new GameObject[hexesList.Count];
		for (int i = 0; i < hexesArray.Length; i ++) {
			hexesArray[i] = hexesList[i];
		}
		return hexesArray;
	}

	// Array of hexes within a certain radius from certain cordinates set to sublists acording to their distance from center.
	static public GameObject[][] HexesArrayRad (int R, int X, int Y) {
		List <List <GameObject>> hexesList = HexesListRad(R, X, Y);
		GameObject[][] hexesArray = new GameObject[hexesList.Count][];
		for (int i = 0; i < hexesArray.Length; i ++) {
			hexesArray[i] = new GameObject[hexesList[i].Count];
			for (int j = 0; j < hexesArray[i].Length; j ++) {
				hexesArray[i][j] = hexesList[i][j];
			}
		}
		return hexesArray;
	}

	// List of hex-cordinates within a certain radius from certain cordinates.
	static public List <int[]> HexesCordinatesList (int R, int X, int Y) {
		List <int[]> hexesList = new List<int[]> ();
		int y = YStart (R, Y);
		int yCap = YEnd (R, Y);
		for(; y <= yCap; y ++) {
			int x = XStart (R, X, Y, y);
			int xCap = XEnd (R, X, Y, y);
			for(; x <= xCap; x ++) {
				hexesList.Add(new int[2] {x, y});
			}
		}
		return hexesList;
	}

	// List of hex-cordinates within a certain radius from certain cordinates set to sublists acording to their distance from center.
	static public List <List <int[]>> HexesCordinatesListRad (int R, int X, int Y) {
		List <List <int[]>> hexesList = new List<List <int[]>> ();
		int y = YStart (R, Y);
		int yCap = YEnd (R, Y);
		for(; y <= yCap; y ++) {
			int x = XStart (R, X, Y, y);
			int xCap = XEnd (R, X, Y, y);
			for(; x <= xCap; x ++) {
				int Distance = PosInRad (X, Y, x, y);
				while(hexesList.Count < Distance + 1) {
					hexesList.Add(new List<int[]> ());
				}
				hexesList[Distance].Add(new int[2] {x, y});
			}
		}
		return hexesList;
	}

	// Array of hex-cordinates within a certain radius from certain cordinates.
	static public int[][] HexesCordinatesArray (int R, int X, int Y) {
		List <int[]> hexesList = HexesCordinatesList(R, X, Y);
		int[][] hexesArray = new int[hexesList.Count][];
		for (int i = 0; i < hexesArray.Length; i ++) {
			hexesArray[i] = hexesList[i];
		}
		return hexesArray;
	}

	// Array of hex-cordinates within a certain radius from certain cordinates set to sublists acording to their distance from center.
	static public int[][][] HexesCordinatesArrayRad (int R, int X, int Y) {
		List <List <int[]>> hexesList = HexesCordinatesListRad(R, X, Y);
		int[][][] hexesArray = new int[hexesList.Count][][];
		for (int i = 0; i < hexesArray.Length; i ++) {
			hexesArray[i] = new int[hexesList[i].Count][];
			for (int j = 0; j < hexesArray[i].Length; j ++) {
				hexesArray[i][j] = hexesList[i][j];
			}
		}
		return hexesArray;
	}

	// Tells how many hexes should be in a certain radius.
	static public int RadiusHexCount (int R) {
		return ((R + 1) / 2) * 6 * R + 1;
	}
}
