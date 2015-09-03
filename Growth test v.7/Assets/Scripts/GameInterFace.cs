using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

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

	public GameObject invalid;
	public GameObject correct;
	
	Text[] text = new Text[10];

	bool hasPlant = false;
	GameObject plant;
	GameObject reflection;
	bool upRoot = false;
	bool atCorrect = false;

	GameObject currentReflection;
	GameObject currentHex;

	bool showButtons = false;

	public GameObject[] seedButtons;
	public GameObject shovelButton;

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

	public void UpRoot () {
		if (!MouseScript.editorInUse) {
			hasPlant = false;
			upRoot = !upRoot;
			if (reflection != correct && currentReflection && upRoot) {
				Destroy (currentReflection);
				currentReflection = null;
			}
			reflection = correct;
			plant = null;
		}
	}

	public void Plant (GameObject buttonPlant) {
		if(!MouseScript.editorInUse) {
			plant = null;
			hasPlant = false;
			upRoot = false;
			if (seeds [buttonPlant.GetComponent<Plant> ().seedIndex] > 0) {
				hasPlant = true;
				plant = buttonPlant;
			}
		}
	}

	public void Reflection (GameObject buttonReflection) {
		if (!MouseScript.editorInUse) {
			if (reflection != buttonReflection && currentReflection) {
				Destroy (currentReflection);
				currentReflection = null;
			}
			reflection = buttonReflection;
		}
	}

	public void ToggleButtons () {
		showButtons = !showButtons;
	}
	
	// Update is called once per frame
	void Update () {
		if ((MouseScript.editorInUse || (!hasPlant && !upRoot)) && currentReflection) {
			Destroy(currentReflection);
			currentReflection = null;
			if(!hasPlant && !upRoot) {
				plant = null;
				reflection = null;
			}
		}

		bool unBlocked = true;
	/*	Ray anotherCamRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit anotherHitPoint;
		if (Physics.Raycast (anotherCamRay, out anotherHitPoint, Mathf.Infinity, 1 << 5)) {
			if (anotherHitPoint.collider.tag == "BlockingUI") {
				unBlocked = false;
			}
		}*/

		if(EventSystem.current.IsPointerOverGameObject())
		//	if(EventSystem.current.currentSelectedGameObject.tag == "BlockingUI")
				unBlocked = false;

		if (hasPlant && !Input.GetKey (KeyCode.LeftShift) && !MouseScript.editorInUse) {
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitPoint;
			if (Physics.Raycast (camRay, out hitPoint, Mathf.Infinity, 1 << 8) && unBlocked) {
				GameObject compHex = hitPoint.collider.gameObject;
				if (compHex != currentHex) {
					currentHex = compHex;
					if(currentReflection) {
						currentReflection.transform.position = currentHex.transform.position;
					}
				}
				if (!currentHex.transform.FindChild ("Plant") && ((!currentReflection && reflection) || !atCorrect)) {
					if (currentReflection) {
						Destroy(currentReflection);
						currentReflection = null;
					}
					if(reflection) {
						currentReflection = (GameObject)Instantiate (reflection);
						currentReflection.transform.position = currentHex.transform.position;
					}
					atCorrect = true;
				} else if (currentHex.transform.FindChild ("Plant") && ((!currentReflection && invalid) || atCorrect)) {
					if (currentReflection) {
						Destroy(currentReflection);
						currentReflection = null;
					}
					if(invalid) {
						currentReflection = (GameObject)Instantiate (invalid);
						currentReflection.transform.position = currentHex.transform.position;
					}
					atCorrect = false;
				}
			}
		}

		if (upRoot && !Input.GetKey (KeyCode.LeftShift) && !MouseScript.editorInUse) {
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitPoint;
			if (Physics.Raycast (camRay, out hitPoint, Mathf.Infinity, 1 << 8) && unBlocked) {
				GameObject compHex = hitPoint.collider.gameObject;
				if (compHex != currentHex) {
					currentHex = compHex;
					if(currentReflection) {
						currentReflection.transform.position = currentHex.transform.position;
					}
				}
				if (!currentHex.transform.FindChild ("Plant") && ((!currentReflection && invalid) || !atCorrect)) {
					if (currentReflection) {
						Destroy(currentReflection);
						currentReflection = null;
					}
					if(invalid) {
						currentReflection = (GameObject)Instantiate (invalid);
						currentReflection.transform.position = currentHex.transform.position;
					}
					atCorrect = true;
				} else if (currentHex.transform.FindChild ("Plant") && ((!currentReflection && correct) || atCorrect)) {
					if (currentReflection) {
						Destroy(currentReflection);
						currentReflection = null;
					}
					if(correct) {
						currentReflection = (GameObject)Instantiate (correct);
						currentReflection.transform.position = currentHex.transform.position;
					}
					atCorrect = false;
				}
			}
		}

		if (hasPlant && !Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Mouse0) && !MouseScript.editorInUse) {
			hasPlant = false;
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitPoint;
			if (Physics.Raycast (camRay, out hitPoint, Mathf.Infinity, 1 << 8) && !hitPoint.collider.transform.FindChild ("Plant") && unBlocked) {
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

		if (upRoot && !Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Mouse0) && !MouseScript.editorInUse) {
			upRoot = false;
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitPoint;
			if (Physics.Raycast (camRay, out hitPoint, Mathf.Infinity, 1 << 8) && hitPoint.collider.transform.FindChild ("Plant") && unBlocked) {
				Destroy(hitPoint.collider.transform.FindChild ("Plant").gameObject);
			}
			if (currentReflection) {
				Destroy(currentReflection);
				currentReflection = null;
			}
			reflection = null;
		}

		for (int i = 0; i < 10; i ++) {
			if(text[i])
				text[i].text = "" + seeds[i];
		}
		if (showButtons != shovelButton.activeSelf) {
			shovelButton.SetActive(showButtons);
			seedButtons[0].transform.parent.gameObject.SetActive(showButtons);
		}
	}
}
