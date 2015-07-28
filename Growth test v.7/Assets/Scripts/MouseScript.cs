using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {

	public Material green;
	public Material brown;
	public Material blue;
	public Material red;
	public Material white;
	public Material black;
	public Material orange;
	public Material yellow;
	public Material pink;
	public Material[] colors;

	public Quaternion hexRot;
	public Vector3 hexPos;

	public GameObject plant;
	public GameObject cameraFocus;
	public GameObject mainCamera;
	public GameObject tilt;

	public float panSpeed;
	float cameraPanSpeed;

	public int materialInUse = 10;
	public int childCount;

	public bool lookMode;

	void Start () {

		//Array of clored materials
		colors = new Material[9];
		
		colors[0] = green;
		colors[1] = brown;
		colors[2] = blue;
		colors[3] = red;
		colors[4] = black;
		colors[5] = orange;
		colors[6] = yellow;
		colors[7] = pink;
		
	}

	void Update () {

		cameraPanSpeed = Vector3.Distance(cameraFocus.transform.position,mainCamera.transform.position) * panSpeed;

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitPoint;

		//LookMode
		//Panning
		if (Input.GetKey (KeyCode.Mouse0)){
			lookMode = true;
//			cameraFocus.transform.position -= new Vector3 (0,0,Input.GetAxis("Mouse Y")) * cameraPanSpeed;
//			cameraFocus.transform.right += new Vector3 (Input.GetAxis("Mouse X"),0,0) * cameraPanSpeed;
			cameraFocus.transform.position += new Vector3 (Input.GetAxis("Mouse X"),0,Input.GetAxis("Mouse Y")) * cameraPanSpeed;
		}
		//Orbiting
		if (Input.GetKey (KeyCode.Mouse1)){
			lookMode = true;
			tilt.transform.rotation *= Quaternion.Euler (-Input.GetAxis("Mouse Y"),0,0);
			cameraFocus.transform.rotation *= Quaternion.Euler (0,Input.GetAxis("Mouse X"),0);
		}

		if (Physics.Raycast (camRay, out hitPoint, 100f)) {

			hexPos = hitPoint.collider.gameObject.transform.position;

			if (lookMode == false) {

				//Coloring the hexes
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && materialInUse < 8) {
					hitPoint.collider.gameObject.GetComponent<Renderer> ().material = colors [materialInUse];
				}
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse1) && materialInUse < 8) {
					hitPoint.collider.gameObject.GetComponent<Renderer> ().material = white;
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

				//Setting and removing plants
				if (hitPoint.collider.gameObject.tag == "Hex" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 10
				    && hitPoint.collider.gameObject.transform.childCount == hitPoint.collider.gameObject.GetComponent<Resourse>().childCount) {

					GameObject plantIns = (GameObject)Instantiate (plant, hexPos, hexRot);
					plantIns.transform.parent = hitPoint.collider.gameObject.transform;
				}
				if (hitPoint.collider.gameObject.tag == "Plant" && Input.GetKeyDown (KeyCode.Mouse1) && materialInUse == 10) {
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
	}
}