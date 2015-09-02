using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameInterFace : MonoBehaviour {

	public static int[] seeds = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

	public Text text0;
	public int seed0 = 5;
	public Text text1;
	public int seed1 = 2;
	public Text text2;
	public int seed2 = 1;
	public Text text3;
	public int seed3 = 0;
	public Text text4;
	public int seed4 = 0;
	public Text text5;
	public int seed5 = 0;
	public Text text6;
	public int seed6 = 0;
	public Text text7;
	public int seed7 = 0;
	public Text text8;
	public int seed8 = 0;
	public Text text9;
	public int seed9 = 0;
	
	Text[] text = new Text[10];

	bool hasPlant = false;
	GameObject plant;
	GameObject reflection;

	GameObject currentReflection;
	GameObject currentHex;

	void Awake () {
		text [0] = text0;
		seeds [0] = seed0;
		text [1] = text1;
		seeds [1] = seed1;
		text [2] = text2;
		seeds [2] = seed2;
		text [3] = text3;
		seeds [3] = seed3;
		text [4] = text4;
		seeds [4] = seed4;
		text [5] = text5;
		seeds [5] = seed5;
		text [6] = text6;
		seeds [6] = seed6;
		text [7] = text7;
		seeds [7] = seed7;
		text [8] = text8;
		seeds [8] = seed8;
		text [9] = text9;
		seeds [9] = seed9;
	}

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
		for (int i = 0; i < 10; i ++) {
			if(text[i])
				text[i].text = "" + seeds[i];
		}
	}
}
