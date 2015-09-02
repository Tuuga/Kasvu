using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {

	public Material[] colors;

	public Quaternion plantRot;

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

	static public bool editorInUse = true;

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
				cameraFocus.transform.rotation *= Quaternion.Euler (0, Input.GetAxis ("Mouse X") * orbitSpeed, 0);
			}
		} else {
			lookMode = false;
		}

		if (Physics.Raycast (camRay, out hitPoint, 100f, 1 << 8)) {

			hexPos = hitPoint.collider.gameObject.transform.position;

			if (lookMode == false && hitPoint.collider.gameObject.tag == "Hex" && editorInUse) {

				//Coloring the hexes
				if (drawMode == true) {

					if (materialInUse < 8) {
						if (Input.GetKey (KeyCode.Mouse0)) {
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

						if (Input.GetKey (KeyCode.Mouse1)) {
							hitPoint.collider.gameObject.GetComponent<Renderer> ().material = colors [8];
						}
					}
				}

				//Resource and plant mode
				if (drawMode == false) {

					//Resource modification
					if (plantInUse == 8) {
						if (Input.GetKey (KeyCode.Mouse0)) {
							hitPoint.collider.gameObject.GetComponent <Resourse> ().water += 1;
						}
						if (Input.GetKey (KeyCode.Mouse1)) {
							hitPoint.collider.gameObject.GetComponent <Resourse> ().water -= 1;
						}
					}
					if (plantInUse == 9) {
						if (Input.GetKey (KeyCode.Mouse0)) {
							hitPoint.collider.gameObject.GetComponent <Resourse> ().nutrients += 1;
						}
						if (Input.GetKey (KeyCode.Mouse1)) {
							hitPoint.collider.gameObject.GetComponent <Resourse> ().nutrients -= 1;
						}
					}

					//Setting plants
					if (Input.GetKey (KeyCode.Mouse0) && plantInUse <= 4 && !hitPoint.collider.transform.FindChild ("Plant")) {

						GameObject plantIns = (GameObject)Instantiate (plants[plantInUse], hexPos,new Quaternion (0,0,0,0));
						plantIns.transform.parent = hitPoint.collider.gameObject.transform;

						plantIns.transform.rotation = plants[plantInUse].transform.rotation;
						plantIns.name = "Plant";
					}
				}
			}
			//Removing plants
			if (hitPoint.collider.gameObject.tag == "Plant" && Input.GetKey (KeyCode.Mouse1) && drawMode == false && editorInUse) {
				Destroy (hitPoint.collider.gameObject);
			}
		}

		//Inputs
		//Setting the index of the material that you want to use

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
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				plantInUse = 1;
			}

			//Plant 3 and 4 not used yet
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				plantInUse = 2;
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				plantInUse = 3;
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
		if (Input.GetKeyDown (KeyCode.Space))
			editorInUse = !editorInUse;
	}
}