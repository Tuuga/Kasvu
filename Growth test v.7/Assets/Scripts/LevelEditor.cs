using UnityEngine;
using UnityEditor;
using System.Collections;

public class LevelEditor : EditorWindow {

	string drawMode = "Draw";
	string editMode = "Edit";
	string rayObject;
	int events = 0;
	Vector2 mouseLastPos;

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

		GUILayout.Label (rayObject);
	}

	void OnEnable () {
		SceneView.onSceneGUIDelegate += OnSceneGUI;
	}

	void OnDisable () {
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
	}

	public void OnSceneGUI (SceneView sceneview) {
		mouseLastPos = Event.current.mousePosition;

		if (Camera.current) {
			Ray camRay = HandleUtility.GUIPointToWorldRay (mouseLastPos);
			RaycastHit hitPoint;
			
			if (Physics.Raycast (camRay, out hitPoint, 100f)) {
				rayObject = hitPoint.collider.name;
				Repaint();
				Resourse r = hitPoint.collider.gameObject.GetComponent<Resourse>();
				if (r) {
					r.water = 10;
				}
			}
		}
		events++; Debug.Log(mouseLastPos);
		if (Event.current.type == EventType.MouseMove) {
			Event.current.Use ();
		}
	}

	void Update () {

//		if (Camera.current) {
//			Ray camRay = HandleUtility.GUIPointToWorldRay (mouseLastPos);
//			RaycastHit hitPoint;
//
//			if (Physics.Raycast (camRay, out hitPoint, 100f)) {
//				rayObject = hitPoint.collider.name;
//				Repaint();
//			}
//		}
	}
}
