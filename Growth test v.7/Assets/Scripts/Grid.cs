﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class Grid: MonoBehaviour
{
	//following public variable is used to store the hex model prefab;
	//instantiate it by dragging the prefab on this variable using unity editor
	public GameObject Hex;
	//next two variables can also be instantiated using unity editor
	public int gridWidthInHexes = 10;
	public int gridHeightInHexes = 10;
	
	//Hexagon tile width and height in game world
	private float hexWidth;
	private float hexHeight;

	[SerializeField]
	GameObject grid;

	public GameObject[][] heksagons;

	[MenuItem ("Grid-O-Matic/One Grid please!")]
	static void GenerateGrid() {
		GameObject.Find ("GM").GetComponent<Grid> ().setSizes();
		GameObject.Find ("GM").GetComponent<Grid> ().createGrid();
	}
	
	//Method to initialise Hexagon width and height
	void setSizes()
	{
		//renderer component attached to the Hex prefab is used to get the current width and height
		hexWidth = Hex.GetComponent<Renderer>().bounds.size.x;
		hexHeight = Hex.GetComponent<Renderer>().bounds.size.z;
	}
	
	//Method to calculate the position of the first hexagon tile
	//The center of the hex grid is (0,0,0)
	Vector3 calcInitPos()
	{
		Vector3 initPos;

		initPos = new Vector3(-hexWidth * gridWidthInHexes / 2f + hexWidth * 3 / 4, 0, gridHeightInHexes / 2f * -hexHeight * 3 / 4 + hexHeight /2 * 3 / 4);
		
		return initPos;
	}
	
	//method used to convert hex grid coordinates to game world coordinates
	public Vector3 calcWorldCoord(Vector2 gridPos)
	{
		//Position of the first hex tile
		Vector3 initPos = calcInitPos();
		//Every second row is offset by half of the tile width
		//float offset = 0;
		//if (gridPos.y % 2 != 0)
		//	offset = hexWidth / 2;
		
		//float x =  initPos.x + offset + gridPos.x * hexWidth;
		//Every new line is offset in z direction by 3/4 of the hexagon height
		//float z = initPos.z + gridPos.y * hexHeight * 0.75f;
		//return new Vector3(x, 0, z);
		initPos += Vector3.right * gridPos.x * hexWidth + Quaternion.AngleAxis (-30, Vector3.up) * Vector3.forward * gridPos.y * hexWidth;
		return initPos;
	}
	
	//Finally the method which initialises and positions all the tiles
	void createGrid()
	{
		if (grid != null) {
			DestroyImmediate(grid);
		}
		//Game object which is the parent of all the hex tiles
		GameObject hexGridGO = new GameObject("HexGrid");
		grid = hexGridGO;
		heksagons = new GameObject[gridWidthInHexes + (gridHeightInHexes - 1) / 2][];
		for (int x = 0; x < heksagons.Length; x ++)
			heksagons[x] = new GameObject[gridHeightInHexes];
		
		for (int y = 0; y < gridHeightInHexes; y++)
		{
			for (int x = 0 + y / 2; x < gridWidthInHexes + y / 2; x++)
			{
				//GameObject assigned to Hex public variable is cloned
				GameObject hex = (GameObject)Instantiate(Hex);
				//Current position in grid
				Vector2 gridPos = new Vector2(x, y);
				hex.transform.position = calcWorldCoord(gridPos);
				hex.transform.parent = hexGridGO.transform;
				heksagons[x][y] = hex;
				hex.GetComponent<Resourse> ().xPos = x;
				hex.GetComponent<Resourse> ().yPos = y;
			}
		}
	}
}