using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	public enum resourceUseType {limit, cap, stockpile};
	public enum plantState {sprouting, adult, flower};

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

	public float requiredTimeToGrow = 10;
	public float requiredTimeToFlower = 10;
	public float requiredTimeToSeed = 10;
	public float requiredTimeToDie = 10;

	plantState currentPlantState = plantState.sprouting;
	float requiredTime = 10;
	float timer = 0;
	float deathTimer = 0;
	Animator anim;
	GameObject[] Hexes;
	int key;
	Grid axisGrid;
	int xPos;
	int yPos;
	bool isDying = false;

	//Resurssi systeemit kommentoitu pois
	void Start () {
		requiredTime = requiredTimeToGrow;
		parentHex = transform.parent.gameObject;
		anim = GetComponent<Animator> ();
		axisGrid = GameObject.Find ("GM").GetComponent<Grid> ();
		Hexes = axisGrid.heksagons;
		key = axisGrid.gridWidthInHexes + (axisGrid.gridHeightInHexes - 1) / 2;
		xPos = parentHex.GetComponent<Resourse> ().xPos;
		yPos = parentHex.GetComponent<Resourse> ().yPos;

		if (nutrientUseType != resourceUseType.stockpile) {
			GameObject[] hexArray = HF.HexesArray(nutrientProduktionRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().nutrients < 100) {
					hexArray[i].GetComponent<Resourse> ().nutrients += nutrientProduktion;
				}
			}
		/*	int X = xPos;
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
			} */
		}

		if (waterUseType != resourceUseType.stockpile) {
			GameObject[] hexArray = HF.HexesArray(waterProduktionRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().water < 100) {
					hexArray[i].GetComponent<Resourse> ().water += waterProduktion;
				}
			}
		/*	int X = xPos;
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
			}*/
		}

		if (nutrientUseType == resourceUseType.limit) {
			GameObject[] hexArray = HF.HexesArray(nutrientUseRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().nutrients > 0) {
					hexArray[i].GetComponent<Resourse> ().nutrients -= nutrientUse;
				}
			}
		/*	int X = xPos;
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
			}*/
		}

		if (waterUseType == resourceUseType.limit) {
			GameObject[] hexArray = HF.HexesArray(waterUseRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().water > 0) {
					hexArray[i].GetComponent<Resourse> ().water -= waterUse;
				}
			}
		/*	int X = xPos;
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
			}*/
		}
	}

	void ResourceLoop (int R, float n, bool rTypeN) {
		int hexesToUse = 1 + (R + 1) / 2 * 6 * R;
		float totalResUse = hexesToUse * n * Time.deltaTime;
		int X = xPos;
		int Y = yPos;
		int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
		int xyzCap = 0;
		float smallNumber = 1f / 1000000f;
		while (totalResUse > smallNumber && hexesToUse > 0 && xyzCap < 1000) {
			float hexResUse = totalResUse / hexesToUse;
			hexesToUse = 0;
			int y = Mathf.Max (Y - R, 0);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					totalResUse -= hexResUse;
					if(rTypeN) {
						Hexes [x + y * key].GetComponent<Resourse> ().nutrients -= hexResUse;
						if (Hexes [x + y * key].GetComponent<Resourse> ().nutrients > 0) {
							hexesToUse += 1;
						} else {
							totalResUse -= Hexes [x + y * key].GetComponent<Resourse> ().nutrients;
							Hexes [x + y * key].GetComponent<Resourse> ().nutrients = 0;
						}
					} else {
						Hexes [x + y * key].GetComponent<Resourse> ().water -= hexResUse;
						if (Hexes [x + y * key].GetComponent<Resourse> ().water > 0) {
							hexesToUse += 1;
						} else {
							totalResUse -= Hexes [x + y * key].GetComponent<Resourse> ().water;
							Hexes [x + y * key].GetComponent<Resourse> ().water = 0;
						}
					}
				}
			}
			xyzCap ++;
			if (xyzCap == 1000) {
				Debug.Log(totalResUse);
				Debug.Log(Mathf.Epsilon);
				Debug.Log(hexesToUse);
				float uy = Mathf.PI;
				float ut = uy / 13;
				Debug.Log(uy - (ut * 13));
				Debug.Log(uy - ((uy / 13) * 13));
			}
		}
		if (totalResUse > smallNumber) {
			isDying = true;
		}
	}

	void Update () {
		float timeAdd = Time.deltaTime;
		isDying = false;
		if (nutrientUseType == resourceUseType.stockpile) {
			ResourceLoop (nutrientUseRadius, nutrientUse, true);
			GameObject[] hexArray = HF.HexesArray(nutrientProduktionRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().nutrients < 100) {
					hexArray[i].GetComponent<Resourse> ().nutrients += nutrientProduktion * Time.deltaTime;
				}
			}
		/*	int X = xPos;
			int Y = yPos;
			int R = nutrientProduktionRadius;//nutrientUseRadius;
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
			}*/
		}

		if (waterUseType == resourceUseType.stockpile) {
			ResourceLoop (waterUseRadius, waterUse, false);
			GameObject[] hexArray = HF.HexesArray(waterProduktionRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().water < 100) {
					hexArray[i].GetComponent<Resourse> ().water += waterProduktion * Time.deltaTime;
				}
			}
		/*	int X = xPos;
			int Y = yPos;
			int R = waterProduktionRadius;//waterUseRadius;
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
			}*/
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Destroy (gameObject);
		}

		if (nutrientUseType != resourceUseType.stockpile) {
			float totalRes = 0;
			int R = nutrientUseRadius;
			GameObject[] hexArray = HF.HexesArray(R, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				totalRes += hexArray [i].GetComponent<Resourse> ().nutrients;
			}
		/*	int X = xPos;
			int Y = yPos;
			int R = nutrientUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					totalRes += Hexes [x + y * key].GetComponent<Resourse> ().nutrients;
					if (Hexes [x + y * key].GetComponent<Resourse> ().nutrients < nutrientUse) {
						isDying = true;
					}
				}
			}*/
			if (totalRes < nutrientUse * (1 + (R + 1) / 2 * 6 * R)) {
				isDying = true;
			}
		}
		if (waterUseType != resourceUseType.stockpile) {
			float totalRes = 0;
			int R = waterUseRadius;
			GameObject[] hexArray = HF.HexesArray(R, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				totalRes += hexArray [i].GetComponent<Resourse> ().water;
			}
		/*	int X = xPos;
			int Y = yPos;
			int R = waterUseRadius;
			int y = Mathf.Max (Y - R, 0);
			int yCap = Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1);
			for (; y <= yCap; y ++) {
				int x = Mathf.Max (X - R, X - R + y - Y, 0 + y / 2);
				int xCap = Mathf.Min (X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1);
				for (; x <= xCap; x ++) {
					totalRes += Hexes [x + y * key].GetComponent<Resourse> ().water;
					if (Hexes [x + y * key].GetComponent<Resourse> ().water < waterUse) {
						isDying = true;
					}
				}
			}*/
			if (totalRes < waterUse * (1 + (R + 1) / 2 * 6 * R)) {
				isDying = true;
			}
		}

		if (isDying) {
			deathTimer += timeAdd;
			timer = 0;
			if (deathTimer >= requiredTimeToDie) {
				Destroy (gameObject);
			}
		} else {
			timer += timeAdd;
			deathTimer = 0;
			if (timer >= requiredTime) {
				timer -= requiredTime;
				anim.SetTrigger ("testTrigger");
				if(currentPlantState == plantState.adult) {
					currentPlantState = plantState.flower;
					requiredTime = requiredTimeToSeed;
				} else {
					currentPlantState = plantState.adult;
					requiredTime = requiredTimeToFlower;
				}
			}
		}
	}

	void OnDestroy () {
		if (nutrientUseType != resourceUseType.stockpile) {
			GameObject[] hexArray = HF.HexesArray(nutrientProduktionRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().nutrients < 100) {
					hexArray[i].GetComponent<Resourse> ().nutrients -= nutrientProduktion;
				}
			}
		/*	int X = xPos;
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
			}*/
		}
		
		if (waterUseType != resourceUseType.stockpile) {
			GameObject[] hexArray = HF.HexesArray(waterProduktionRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().water < 100) {
					hexArray[i].GetComponent<Resourse> ().water -= waterProduktion;
				}
			}
		/*	int X = xPos;
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
			}*/
		}
		
		if (nutrientUseType == resourceUseType.limit) {
			GameObject[] hexArray = HF.HexesArray(nutrientUseRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().nutrients > 0) {
					hexArray[i].GetComponent<Resourse> ().nutrients += nutrientUse;
				}
			}
		/*	int X = xPos;
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
			}*/
		}
		
		if (waterUseType == resourceUseType.limit) {
			GameObject[] hexArray = HF.HexesArray(waterUseRadius, xPos, yPos);
			for(int i = 0; i < hexArray.Length; i ++) {
				if (hexArray[i].GetComponent<Resourse> ().water > 0) {
					hexArray[i].GetComponent<Resourse> ().water += waterUse;
				}
			}
		/*	int X = xPos;
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
			}*/
		}
	}
}
