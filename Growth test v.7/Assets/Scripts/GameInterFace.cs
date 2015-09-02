using UnityEngine;
using System.Collections;

public class GameInterFace : MonoBehaviour {

	public static int[] seeds = {1000000, 0, 0, 0, 0, 0, 0, 0, 0, 0};

	bool hasPlant = false;
	GameObject plant;
	GameObject reflection;

	GameObject currentReflection;
	GameObject currentHex;

	// Use this for initialization
	void Start () {

	}

	public void Plant (GameObject buttonPlant) {
		if (seeds [buttonPlant.GetComponent<Plant> ().seedIndex] > 0 && !MouseScript.editorInUse) {
			hasPlant = true;
			plant = buttonPlant;
		}
	}

	public void Reflection (GameObject buttonReflection) {
		if (reflection != buttonReflection && currentReflection) {
			Destroy(currentReflection);
			currentReflection = null;
		}
		reflection = buttonReflection;
	}
	
	// Update is called once per frame
	void Update () {
		if ((MouseScript.editorInUse || !hasPlant) && currentReflection) {
			Destroy(currentReflection);
			currentReflection = null;
		}

		if (hasPlant && !Input.GetKey (KeyCode.LeftShift) && !MouseScript.editorInUse) {
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitPoint;
			if (Physics.Raycast (camRay, out hitPoint,  1 << 8) && !hitPoint.collider.transform.FindChild ("Plant")) {
				GameObject compHex = hitPoint.collider.gameObject;
				if (compHex != currentHex) {
					currentHex = compHex;
					if(currentReflection) {
						currentReflection.transform.position = currentHex.transform.position;
					}
				}
				if (!currentReflection && reflection) {
					currentReflection = (GameObject)Instantiate (reflection);
					currentReflection.transform.position = currentHex.transform.position;
				}
			}
		}

		if (hasPlant && !Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Mouse0) && !MouseScript.editorInUse) {
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
			if (currentReflection) {
				Destroy(currentReflection);
				currentReflection = null;
			}
			plant = null;
			reflection = null;
		}
	}
}
