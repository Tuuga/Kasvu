using UnityEngine;
using System.Collections;

public class LevelManagerScript : MonoBehaviour {

	public Transform[] mark;
	int markInt;
	public GameObject cameraObj;
	public float speed;
	bool moveCameraRight;
	bool moveCameraLeft;
	bool startBool = false;

	void Start () {
		RightArrow ();
	}

	void Update () {

		if (moveCameraRight && startBool) {
			cameraObj.transform.localPosition += new Vector3 (1, 0, 0) * Time.deltaTime * speed;
		}
		if (moveCameraLeft) {
			cameraObj.transform.localPosition += new Vector3 (-1, 0, 0) * Time.deltaTime * speed;
		}
		if (moveCameraRight && !startBool) {
			cameraObj.transform.localPosition += new Vector3 (1, 0, 0) * Time.deltaTime * 10;
		}

		if (Vector3.Distance (cameraObj.transform.position, mark [markInt].position) < 1f) {
			moveCameraRight = false;
			moveCameraLeft = false;
			startBool = true;
		}
	}

	public void StartLevel () {
		Application.LoadLevel (markInt + 1);
	}

	public void LeftArrow () {
		if (!moveCameraLeft && !moveCameraRight) {
			if (markInt <= 5 && markInt > 1) {
				markInt = Mathf.Clamp (markInt - 1, 0, 5);
				Debug.Log (markInt);
				moveCameraLeft = true;
			}
		}
	}

	public void RightArrow () {
		if (!moveCameraLeft && !moveCameraRight) {
			if (markInt < 5 && markInt >= 0) {
				markInt = Mathf.Clamp (markInt + 1, 0, 5);
				Debug.Log (markInt);
				moveCameraRight = true;
			}
		}
	}
}
