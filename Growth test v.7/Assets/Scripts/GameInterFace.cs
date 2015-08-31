using UnityEngine;
using System.Collections;

public class GameInterFace : MonoBehaviour {

	public static int[] seeds = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

	bool hasPlant = false;
	GameObject plant;

	// Use this for initialization
	void Start () {

	}

	public void Plant1 (GameObject buttonPlant) {
		if (seeds [buttonPlant.GetComponent<Plant> ().seedIndex] > 0) {
			hasPlant = true;
			plant = buttonPlant;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hasPlant && !Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Mouse0)) {
			hasPlant = false;
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitPoint;
			if (Physics.Raycast (camRay, out hitPoint,  1 << 8) && !hitPoint.collider.transform.FindChild ("Plant")) {
				seeds[plant.GetComponent<Plant>().seedIndex] -= 1;
				GameObject plantIns = (GameObject)Instantiate (plant);
				plantIns.transform.position = hitPoint.collider.gameObject.transform.position;
				plantIns.transform.parent = hitPoint.collider.gameObject.transform;
				plantIns.name = "Plant";
			}
			plant = null;
		}
	}
}
