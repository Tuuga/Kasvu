using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	public enum resourceUseType {limit, cap, stockpile};

	static public resourceUseType nutrientUseType = resourceUseType.stockpile;
	static public resourceUseType waterUseType = resourceUseType.stockpile;

	public GameObject parentHex;

	public float nutrientUse = 10;
	public int nutrientUseRadius = 1;
	public float waterUse = 10;
	public int waterUseRadius = 1;
	
	public float nutrientProduktion = 10;
	public int nutrientProduktionRadius = 1;
	public float waterProduktion = 10;
	public int waterProduktionRadius = 1;

	public float requiredTime = 10;

	float timer = 0;
	Animator anim;
	GameObject[] Hexes;
	int key;
	Grid axisGrid;
	int xPos;
	int yPos;

	void Start () {
		parentHex = transform.parent.gameObject;
		anim = GetComponent<Animator> ();
		axisGrid = GameObject.Find ("GM").GetComponent<Grid> ();
		Hexes = axisGrid.heksagons;
		key = axisGrid.gridWidthInHexes + (axisGrid.gridHeightInHexes - 1) / 2;
		xPos = parentHex.GetComponent<Resourse> ().xPos;
		yPos = parentHex.GetComponent<Resourse> ().yPos;

		if (nutrientUseType != resourceUseType.stockpile) {
			int X = xPos;
			int Y = yPos;
			int R = nutrientProduktionRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for(; y <= yCap; y ++) {
				int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for(; x <= xCap; x ++) {
					if (Hexes[x + y * key].GetComponent<Resourse> ().nutrients < 100) {
						Hexes[x + y * key].GetComponent<Resourse> ().nutrients += nutrientProduktion;
					}
				}
			}
		}

		if (waterUseType != resourceUseType.stockpile) {
			int X = xPos;
			int Y = yPos;
			int R = waterProduktionRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().water < 100) {
						Hexes [x + y * key].GetComponent<Resourse> ().water += waterProduktion;
					}
				}
			}
		}

		if (nutrientUseType == resourceUseType.limit) {
			int X = xPos;
			int Y = yPos;
			int R = nutrientUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().nutrients > 0) {
						Hexes [x + y * key].GetComponent<Resourse> ().nutrients -= nutrientUse;
					}
				}
			}
		}

		if (waterUseType == resourceUseType.limit) {
			int X = xPos;
			int Y = yPos;
			int R = waterUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().water > 0) {
						Hexes [x + y * key].GetComponent<Resourse> ().water -= waterUse;
					}
				}
			}
		}
	}

	void Update () {
		float timeAdd = Time.deltaTime;
		bool isDying = false;
		if (nutrientUseType == resourceUseType.stockpile) {
			int X = xPos;
			int Y = yPos;
			int R = nutrientUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().nutrients > 0) {
						Hexes [x + y * key].GetComponent<Resourse> ().nutrients -= nutrientUse * Time.deltaTime;
					} else {
						isDying = true;
					}
				}
			}

			R = nutrientProduktionRadius;
			y = Mathf.Max (Y - R, 0);
			yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for(; y <= yCap; y ++) {
				int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for(; x <= xCap; x ++) {
					if (Hexes[x + y * key].GetComponent<Resourse> ().nutrients < 100) {
						Hexes[x + y * key].GetComponent<Resourse> ().nutrients += nutrientProduktion * Time.deltaTime;
					}
				}
			}
		}

		if (waterUseType == resourceUseType.stockpile) {
			int X = xPos;
			int Y = yPos;
			int R = waterUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().water > 0) {
						Hexes [x + y * key].GetComponent<Resourse> ().water -= waterUse * Time.deltaTime;
					} else {
						isDying = true;
					}
				}
			}

			R = waterProduktionRadius;
			y = Mathf.Max (Y - R, 0);
			yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().water < 100) {
						Hexes [x + y * key].GetComponent<Resourse> ().water += waterProduktion * Time.deltaTime;
					}
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Destroy (gameObject);
		}

		if (nutrientUseType != resourceUseType.stockpile) {
			int X = xPos;
			int Y = yPos;
			int R = nutrientUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().nutrients < nutrientUse) {
						isDying = true;
					}
				}
			}
		}
		if (waterUseType != resourceUseType.stockpile) {
			int X = xPos;
			int Y = yPos;
			int R = waterUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().water < waterUse) {
						isDying = true;
					}
				}
			}
		}

		if (isDying) 
			timeAdd = -timeAdd;

		timer += timeAdd;

		if (timer >= requiredTime) {
			timer -= requiredTime;
			if (anim.GetBool ("hasNutrients") == false) {
				anim.SetBool ("hasNutrients", true);
			} else if (anim.GetBool ("canFlower") == false) {
				anim.SetBool ("producedSeed", false);
				anim.SetBool ("canFlower", true);
			} else {
				anim.SetBool ("canFlower", false);
				anim.SetBool ("producedSeed", true);
			}
		} else if (timer <= -requiredTime) {
			Destroy (gameObject);
		}
	}

	void OnDestroy () {
		if (nutrientUseType != resourceUseType.stockpile) {
			int X = xPos;
			int Y = yPos;
			int R = nutrientProduktionRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for(; y <= yCap; y ++) {
				int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for(; x <= xCap; x ++) {
					if (Hexes[x + y * key].GetComponent<Resourse> ().nutrients < 100) {
						Hexes[x + y * key].GetComponent<Resourse> ().nutrients -= nutrientProduktion;
					}
				}
			}
		}
		
		if (waterUseType != resourceUseType.stockpile) {
			int X = xPos;
			int Y = yPos;
			int R = waterProduktionRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().water < 100) {
						Hexes [x + y * key].GetComponent<Resourse> ().water -= waterProduktion;
					}
				}
			}
		}
		
		if (nutrientUseType == resourceUseType.limit) {
			int X = xPos;
			int Y = yPos;
			int R = nutrientUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().nutrients > 0) {
						Hexes [x + y * key].GetComponent<Resourse> ().nutrients += nutrientUse;
					}
				}
			}
		}
		
		if (waterUseType == resourceUseType.limit) {
			int X = xPos;
			int Y = yPos;
			int R = waterUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					if (Hexes [x + y * key].GetComponent<Resourse> ().water > 0) {
						Hexes [x + y * key].GetComponent<Resourse> ().water += waterUse;
					}
				}
			}
		}
	}
}
