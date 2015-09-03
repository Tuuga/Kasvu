using UnityEngine;
using System.Collections;

public class LevelManagerScript : MonoBehaviour {

	public Transform[] mark;
	int markInt;
	public GameObject cameraObj;
	public float speed;
	bool moveCameraRight;
	bool moveCameraLeft;

	void Update () {

		if (moveCameraRight) {
			cameraObj.transform.localPosition += new Vector3 (1, 0, 0) * Time.deltaTime * speed;
		}
		if (Vector3.Distance (cameraObj.transform.position, mark[markInt].position) < 0.5f) {
			moveCameraRight = false;
		}
		if (moveCameraLeft) {
			cameraObj.transform.localPosition += new Vector3 (-1, 0, 0) * Time.deltaTime * speed;
		}
		if (Vector3.Distance (cameraObj.transform.position, mark[markInt].position) < 0.5f) {
			moveCameraLeft = false;
		}
	}

	public void LeftArrow () {
		markInt = Mathf.Clamp (markInt - 1, 0, 5);
		if (markInt < 5 && markInt > 0) {
			moveCameraLeft = true;
		}
	}

	public void RightArrow () {
		if (markInt < 5 && markInt >= 0) {
			Debug.Log (markInt);
			markInt = Mathf.Clamp (markInt + 1, 0, 5);
			Debug.Log (markInt);
			moveCameraRight = true;
		}
	}
}
