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
	public Vector3 cursorPos;

	public GameObject plant;
	public GameObject cameraFocus;

	public int materialInUse = 10;
	public int childCount;

	void Start () {
		
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

//		Debug.Log (Input.GetAxis("Mouse X"));
//		Debug.Log (Input.GetAxis("Mouse Y"));

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitPoint;
		if (Physics.Raycast (camRay, out hitPoint, 100f)) {

			hexPos = hitPoint.collider.gameObject.transform.position;
			cursorPos = hitPoint.point;


			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse0) && materialInUse < 8) {
//				hitPoint.collider.gameObject.tag = "aim";
				hitPoint.collider.gameObject.GetComponent<Renderer> ().material = colors [materialInUse];
			}
			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse1) && materialInUse < 8) {
//				hitPoint.collider.gameObject.tag = "notaim";
				hitPoint.collider.gameObject.GetComponent<Renderer> ().material = white;
			}
			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse2) && materialInUse < 8) {
//				hitPoint.collider.gameObject.tag = "aim";
				hitPoint.collider.gameObject.GetComponent<Renderer> ().material = colors [Random.Range (0, 8)];
			}


			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 8) {
				hitPoint.collider.gameObject.GetComponent <Resourse> ().water += 1;
			}
			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse1) && materialInUse == 8) {
				hitPoint.collider.gameObject.GetComponent <Resourse> ().water -= 1;
			}
			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 9) {
				hitPoint.collider.gameObject.GetComponent <Resourse> ().nutrients += 1;
			}
			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse1) && materialInUse == 9) {
				hitPoint.collider.gameObject.GetComponent <Resourse> ().nutrients -= 1;
			}

			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 10 && Input.GetKeyDown(KeyCode.Space) == false
			    && hitPoint.collider.gameObject.transform.childCount == hitPoint.collider.gameObject.GetComponent<Resourse>().childCount) {

				GameObject plantIns = (GameObject)Instantiate (plant, hexPos, hexRot);
				plantIns.transform.parent = hitPoint.collider.gameObject.transform;
			}
			if (hitPoint.collider.gameObject.tag == "Plant" && Input.GetKeyDown (KeyCode.Mouse1) && materialInUse == 10 && Input.GetKeyDown(KeyCode.Space) == false) {
				Destroy (hitPoint.collider.gameObject);
			}

		}
		if (Input.GetKey(KeyCode.Space) && Input.GetKey (KeyCode.Mouse0)){
			Debug.Log ("Space + M1");
			cameraFocus.transform.position += -cursorPos;
		}

		cursorPos = hitPoint.point;

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
		if (Input.GetKeyDown (KeyCode.Alpha9)) {
			materialInUse = 8;
			Debug.Log ("Water");
		}
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			materialInUse = 9;
			Debug.Log ("Nutrients");
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			materialInUse = 10;
			Debug.Log ("Plant");
		}
	}
}