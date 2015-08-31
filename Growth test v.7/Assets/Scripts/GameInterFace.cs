using UnityEngine;
using System.Collections;

public class GameInterFace : MonoBehaviour {

	public GameObject[] plants;

	bool hasIndex = false;
	int index = 0;

	// Use this for initialization
	void Start () {

	}

	public void Plant1 () {
		hasIndex = true;
		index = 0;
	}

	void Plant2 () {
		hasIndex = true;
		index = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasIndex && !Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Mouse0)) {
			hasIndex = false;
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitPoint;
			if (Physics.Raycast (camRay, out hitPoint,  1 << 8) && !hitPoint.collider.transform.FindChild ("Plant")) {
				GameObject plantIns = (GameObject)Instantiate (plants[index]);
				plantIns.transform.position = hitPoint.collider.gameObject.transform.position;
				plantIns.transform.parent = hitPoint.collider.gameObject.transform;
				plantIns.name = "Plant";
			}
		}
	}
}
