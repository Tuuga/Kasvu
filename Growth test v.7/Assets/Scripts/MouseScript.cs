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
	
	public int materialInUse = 0;

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

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitPoint;
		if (Physics.Raycast (camRay, out hitPoint, 100f)) {
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
				hitPoint.collider.gameObject.GetComponent <Recourse> ().water += 1;
			}
			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse1) && materialInUse == 8) {
				hitPoint.collider.gameObject.GetComponent <Recourse> ().water -= 1;
			}
			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse0) && materialInUse == 9) {
				hitPoint.collider.gameObject.GetComponent <Recourse> ().nutrients += 1;
			}
			if (hitPoint.collider.gameObject.tag == "notaim" && Input.GetKey (KeyCode.Mouse1) && materialInUse == 9) {
				hitPoint.collider.gameObject.GetComponent <Recourse> ().nutrients -= 1;
			}



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
		}
	}
}