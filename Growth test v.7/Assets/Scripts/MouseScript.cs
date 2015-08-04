using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {
	
	public Material[] colors;

	public Quaternion hexRot;
	public Vector3 hexPos;

	public GameObject plant;
	public GameObject plant2;
	public GameObject cameraFocus;
	public GameObject mainCamera;
	public GameObject tilt;

	public float panSpeed;
	public float orbitSpeed;
	float cameraPanSpeed;

	public int materialInUse = 10;
	public int childCount;

	public int radius = 1;
	GameObject[] Hexes;
	int key;
	Grid axisGrid;

	public bool lookMode;

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
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse2) && materialInUse < 8) {
					hitPoint.collider.gameObject.GetComponent<Renderer> ().material = colors [Random.Range (0, 8)];
				}

				//Resource modification
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 8) {
					hitPoint.collider.gameObject.GetComponent <Resourse> ().water += 1;
				}
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse1) && materialInUse == 8) {
					hitPoint.collider.gameObject.GetComponent <Resourse> ().water -= 1;
				}
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 9) {
					hitPoint.collider.gameObject.GetComponent <Resourse> ().nutrients += 1;
				}
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse1) && materialInUse == 9) {
					hitPoint.collider.gameObject.GetComponent <Resourse> ().nutrients -= 1;
				}
//				hitPoint.collider.gameObject.transform.childCount < hitPoint.collider.gameObject.GetComponent<Resourse>().childCount
				//Setting and removing plants
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 10
				    && !hitPoint.collider.gameObject.transform.FindChild ("Plant")) {

					GameObject plantIns = (GameObject)Instantiate (plant, hexPos, hexRot);
					plantIns.transform.parent = hitPoint.collider.gameObject.transform;
					plantIns.transform.Rotate(0, Random.Range(0, 360), 0);
					plantIns.name = "Plant";
					float randomScale = Random.Range (0.5f , 1.5f);
					Vector3 plantScale = new Vector3 (randomScale,randomScale,randomScale);
					plantIns.transform.localScale = (plantScale);
				}
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 11
				    && !hitPoint.collider.gameObject.transform.FindChild ("Plant")) {
					
					GameObject plantIns = (GameObject)Instantiate (plant2, hexPos, hexRot);
					plantIns.transform.parent = hitPoint.collider.gameObject.transform;
					plantIns.transform.Rotate(0, Random.Range(0, 360), 0);
					plantIns.name = "Plant";
					float randomScale = Random.Range (0.5f , 1.5f);
					Vector3 plantScale = new Vector3 (randomScale,randomScale,randomScale);
					plantIns.transform.localScale = (plantScale);
				}
				if (hitPoint.collider.gameObject.tag == "Plant" && Input.GetKey (KeyCode.Mouse1) && materialInUse >= 10) {
					Destroy (hitPoint.collider.gameObject);
				}
			}
		}

		//Setting the index of the material that you want to use
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
		//9 for adding or removing water from a hex
		if (Input.GetKeyDown (KeyCode.Alpha9)) {
			materialInUse = 8;
			Debug.Log ("Water");
		}
		//9 for adding or removing nutrients from a hex
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			materialInUse = 9;
			Debug.Log ("Nutrients");
		}
		//P for adding or removing plants from a hex
		if (Input.GetKeyDown (KeyCode.P)) {
			materialInUse = 10;
			Debug.Log ("Plant");
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			materialInUse = 11;
			Debug.Log ("PlantB");
		}
		if (Input.GetKeyDown (KeyCode.Period)) {
			radius ++;
		}
		if (Input.GetKeyDown (KeyCode.Comma)) {
			radius --;
		}
	}
}