using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {
	
	public Material[] colors;

	public Quaternion hexRot;
	public Vector3 hexPos;

	public GameObject[] plants;
	public GameObject cameraFocus;
	public GameObject mainCamera;
	public GameObject tilt;

	public float panSpeed;
	public float orbitSpeed;
	float cameraPanSpeed;

	public int materialInUse;
	public int plantInUse;

	public int childCount;

	public int radius = 1;
	GameObject[] Hexes;
	int key;
	Grid axisGrid;

	public bool lookMode;
	public bool drawMode;
	bool setLife;

	void Start () {

		axisGrid = GameObject.Find ("GM").GetComponent<Grid> ();
		Hexes = axisGrid.heksagons;
		key = axisGrid.gridWidthInHexes + (axisGrid.gridHeightInHexes - 1) / 2;

	}

	void Update () {

		cameraPanSpeed = Vector3.Distance(cameraFocus.transform.position,mainCamera.transform.position) * panSpeed;

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitPoint;

		//LookMode
		//Panning
		if (Input.GetKey (KeyCode.LeftShift)) {
			lookMode = true;

			if (Input.GetKey (KeyCode.Mouse0)) {
				cameraFocus.transform.position -= cameraFocus.transform.forward * Input.GetAxis("Mouse Y") * cameraPanSpeed;
				cameraFocus.transform.position -= cameraFocus.transform.right * Input.GetAxis("Mouse X") * cameraPanSpeed;
			}
			//Orbiting
			if (Input.GetKey (KeyCode.Mouse1)) {
//				tilt.transform.rotation *= Quaternion.Euler (-Input.GetAxis ("Mouse Y") * orbitSpeed, 0, 0);
				cameraFocus.transform.rotation *= Quaternion.Euler (0, Input.GetAxis ("Mouse X") * orbitSpeed, 0);
			}
		} else {
			lookMode = false;
		}

		if (Physics.Raycast (camRay, out hitPoint, 100f)) {

			hexPos = hitPoint.collider.gameObject.transform.position;

			if (lookMode == false) {

				//Coloring the hexes
				if (drawMode == true) {
					if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && materialInUse < 8) {
						hitPoint.collider.gameObject.GetComponent<Renderer> ().material = colors [materialInUse];
						int X = hitPoint.collider.gameObject.GetComponent<Resourse> ().xPos;
						int Y = hitPoint.collider.gameObject.GetComponent<Resourse> ().yPos;
						int R = radius;
						for(int y = Mathf.Max (Y - R, 0); y <= Mathf.Min (Y + R, axisGrid.gridHeightInHexes - 1); y ++) {
							for(int x = Mathf.Max(X - R, X - R + y - Y, 0 + y / 2); x <= Mathf.Min(X + R, X + R + y - Y, axisGrid.gridWidthInHexes + y / 2 - 1); x ++) {
								Hexes[x + y * key].GetComponent<Renderer> ().material = colors [materialInUse];
							}
						}
					}


					if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse1) && materialInUse < 8) {
						hitPoint.collider.gameObject.GetComponent<Renderer> ().material = colors [8];
					}
				}




				//Resource and plant mode

				if (drawMode == false) {

					//Resource modification
					if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && plantInUse == 8) {
						hitPoint.collider.gameObject.GetComponent <Resourse> ().water += 1;
					}
					if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse1) && plantInUse == 8) {
						hitPoint.collider.gameObject.GetComponent <Resourse> ().water -= 1;
					}
					if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && plantInUse == 9) {
						hitPoint.collider.gameObject.GetComponent <Resourse> ().nutrients += 1;
					}
					if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse1) && plantInUse == 9) {
						hitPoint.collider.gameObject.GetComponent <Resourse> ().nutrients -= 1;
					}

					//Setting plants
					if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0)
					    && !hitPoint.collider.transform.FindChild ("Plant")&& hitPoint.collider.GetComponent<Resourse>().lifeCanGrow == true) {

						GameObject plantIns = (GameObject)Instantiate (plants[plantInUse], hexPos, hexRot);
						plantIns.transform.parent = hitPoint.collider.gameObject.transform;
						plantIns.transform.Rotate(0, Random.Range(0, 360), 0);
						plantIns.name = "Plant";
						float randomScale = Random.Range (0.5f , 1.5f);
						Vector3 plantScale = new Vector3 (randomScale,randomScale,randomScale);
						plantIns.transform.localScale = (plantScale);
					}

					//Removing plants
					if (hitPoint.collider.gameObject.tag == "Plant" && Input.GetKey (KeyCode.Mouse1)) {
						Destroy (hitPoint.collider.gameObject);
					}
					//Setting life manually
					if (hitPoint.collider.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && setLife == true) {
						hitPoint.collider.GetComponent<Resourse>().lifeCanGrow = true;
					}
					//Destroying life
					if (hitPoint.collider.tag == "Life" && Input.GetKey (KeyCode.Mouse1) && setLife == true) {
						hitPoint.collider.transform.parent.GetComponent<Resourse>().lifeCanGrow = false;
						Destroy (hitPoint.collider.gameObject);
					}
				}

				//Plant 2
//				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0)
//				    && !hitPoint.collider.transform.FindChild ("Plant") && hitPoint.collider.GetComponent<Resourse>().lifeCanGrow == true) {
//					
//					GameObject plantIns = (GameObject)Instantiate (plants[1], hexPos, hexRot);
//					plantIns.transform.parent = hitPoint.collider.gameObject.transform;
//					plantIns.transform.Rotate(0, Random.Range(0, 360), 0);
//					plantIns.name = "Plant";
//					float randomScale = Random.Range (0.5f , 1.5f);
//					Vector3 plantScale = new Vector3 (randomScale,randomScale,randomScale);
//					plantIns.transform.localScale = (plantScale);
//				}
				//Destroying plants

				//Setting and removing plants														Can place a plant only if life is growing on that hex
//				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && hitPoint.collider.gameObject.GetComponent<Resourse>().lifeCanGrow == true && materialInUse == 11
//				    && hitPoint.collider.gameObject.transform.childCount == hitPoint.collider.gameObject.GetComponent<Resourse>().childCount) {
//					
//					GameObject plantIns = (GameObject)Instantiate (plant2, hexPos, hexRot);
//					plantIns.transform.parent = hitPoint.collider.gameObject.transform;
//					plantIns.transform.Rotate(0, Random.Range(0, 360), 0);
//				}
//				if (hitPoint.collider.gameObject.tag == "Plant" && Input.GetKey (KeyCode.Mouse1) && materialInUse == 11) {
//					Destroy (hitPoint.collider.gameObject);
//				}
			}
		}

		//Inputs
		//Setting the index of the material that you want to use
		if (Input.GetKeyDown(KeyCode.M)) {
			drawMode = !drawMode;
			if (drawMode) {
				Debug.Log ("Draw Mode");
			}
			if (!drawMode) {
				Debug.Log ("Plant Mode");
			}
		}
		if (drawMode) {
			//var intCorrespondingToEnumRangeStart = (int)KeyCode.Alpha0;
			//var intCorrespondingToEnumRangeEnd = (int)KeyCode.Alpha8;
			// loop int i = rangeStart ... rangeEnd
			//        KeyCode enumValue = (KeyCode)i;
			//        int number = enumValue - rangeStart; // Alpha0 -> 0, Alpha1 -> 1, ...
			
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				materialInUse = 0;
				Debug.Log ("Green");
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				materialInUse = 1;
				Debug.Log ("Brown");
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				materialInUse = 2;
				Debug.Log ("Blue");
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				materialInUse = 3;
				Debug.Log ("Red");
			}
			if (Input.GetKeyDown (KeyCode.Alpha5)) {
				materialInUse = 4;
				Debug.Log ("Black");
			}
			if (Input.GetKeyDown (KeyCode.Alpha6)) {
				materialInUse = 5;
				Debug.Log ("Orange");
			}
			if (Input.GetKeyDown (KeyCode.Alpha7)) {
				materialInUse = 6;
				Debug.Log ("Yellow");
			}
			if (Input.GetKeyDown (KeyCode.Alpha8)) {
				materialInUse = 7;
				Debug.Log ("Pink");
			}

			if (Input.GetKeyDown (KeyCode.Period)) {
				radius ++;
			}
			if (Input.GetKeyDown (KeyCode.Comma)) {
				radius --;
			}
		}

		//P and O for adding or removing plants from a hex
		if (!drawMode) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				plantInUse = 0;
				Debug.Log ("Plant 1");
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				plantInUse = 1;
				Debug.Log ("Plant 2");
			}

			//Plant 3 and 4 not used yet
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				plantInUse = 2;
				Debug.Log ("Plant 3");
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				plantInUse = 3;
				Debug.Log ("Plant 4");
			}

			//Life modification
			if (Input.GetKeyDown (KeyCode.Alpha8)) {
				setLife = !setLife;
				if (setLife) {
					Debug.Log ("Growing out of life: ON");
				}
				if (!setLife) {
					Debug.Log ("Growing out of life: OFF");
				}
			}
			//Resource modification
			if (Input.GetKeyDown (KeyCode.Alpha9)) {
				plantInUse = 8;
				Debug.Log ("Water");
			}
			if (Input.GetKeyDown (KeyCode.Alpha0)) {
				plantInUse = 9;
				Debug.Log ("Nutrients");
			}
		}
	}
}