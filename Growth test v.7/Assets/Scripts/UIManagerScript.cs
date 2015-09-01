using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	GameObject[] highlights;
	public GameObject hexHighlight;
	public GameObject resUI;
	GameObject GM;
	GameObject InputModeText;

	[HideInInspector]
	public GameObject[] resUIarray;

	public Color waterColor;
	public Color nutrientColor;

	[HideInInspector]
	public string waterColorRGBA;
	[HideInInspector]
	public string nutrientColorRGBA;

	bool highlightToggle = true;
	bool toggle;

	void Start () {

		waterColorRGBA = "#" + waterColor.ToHexStringRGBA();
		nutrientColorRGBA ="#" + nutrientColor.ToHexStringRGBA();

		InputModeText = GameObject.Find ("ModeToggle").transform.FindChild("Text").gameObject;
		GM = GameObject.Find ("GM");

		for (int i = 0; i < GM.GetComponent<Grid>().heksagons.Length; i++) {
			if (GM.GetComponent<Grid> ().heksagons [i] != null) {
				GameObject hexHighlightIns = (GameObject)Instantiate (hexHighlight, GM.GetComponent<Grid> ().heksagons [i].transform.position, new Quaternion (0, 0, 0, 0));
				GameObject resUIIns = (GameObject)Instantiate (resUI, GM.GetComponent<Grid> ().heksagons [i].transform.position, new Quaternion (0, 0, 0, 0));

				resUIIns.transform.parent = GM.GetComponent<Grid> ().heksagons [i].transform;
				hexHighlightIns.transform.parent = GM.GetComponent<Grid> ().heksagons [i].transform;
			}
		}
		highlights = GameObject.FindGameObjectsWithTag ("Highlight");
		HexHighlight ();
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) {
			HexHighlight();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)) {
			Text plantInUse = GameObject.Find("PlantInUse").GetComponent<Text>();
			float plantNumber = GM.GetComponent<MouseScript>().plantInUse + 1;
			plantInUse.text = "Plant " + plantNumber;
		}
	}

	public void HexHighlight () {
		highlightToggle = !highlightToggle;
		for (int i = 0; i < highlights.Length; i++){
			highlights[i].SetActive(highlightToggle);
		}
	}

	public void ModeToggle () {

		GM.GetComponent<MouseScript>().drawMode = !GM.GetComponent<MouseScript>().drawMode;

		if (GM.GetComponent<MouseScript>().drawMode == true) {
			InputModeText.GetComponent<Text>().text = "Draw Mode";
		} else {
			InputModeText.GetComponent<Text>().text = "Plant Mode";
		}
	}

	public void ResUIToggle () {

		Text resUIButton = GameObject.Find("ResUIToggle").transform.FindChild("Text").GetComponent<Text>();
		if (toggle) {
			resUIButton.text = "Res UI ON";
		}else {
			resUIButton.text = "Res UI OFF";
		}

		if (resUIarray.Length < 1) {
			resUIarray = GameObject.FindGameObjectsWithTag ("ResUI");
		}
		for (int i = 0; i < resUIarray.Length; i++) {
			resUIarray[i].SetActive(toggle);
		}
		toggle = !toggle;
//		Debug.Log (toggle);
//		Debug.Log (resUIarray.Length);
	}
}
