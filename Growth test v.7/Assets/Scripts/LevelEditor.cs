using UnityEngine;
using UnityEditor;
using System.Collections;

public class LevelEditor : EditorWindow {

	string drawMode = "Draw";
	string editMode = "Edit";

	[MenuItem ("Level Editor/Editor Window")]
	static void Init () {
		LevelEditor window = 
			(LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
	}

	void OnGUI () {

		if (GUILayout.Button (editMode)) {
		}

		if (GUILayout.Button (drawMode)) {
		}

	}

	void Update () {

//		Ray camRay = Camera.current.ScreenPointToRay (Input.mousePosition);
//		RaycastHit hitPoint;
//
//		if (Physics.Raycast (camRay, out hitPoint, 100f)) {
//			Debug.Log (hitPoint.collider.name);
//		}
	}
}
