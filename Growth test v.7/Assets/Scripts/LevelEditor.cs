using UnityEngine;
using UnityEditor;
using System.Collections;

public class LevelEditor : EditorWindow {

	string sceneString = "Scene";
	string waterString = "Water";
	string nutrientString = "Nutrient";
	bool sceneBool = true;
	bool waterBool = false;
	bool nutrientBool = false;

	float editFloat;

	GameObject rayObject;
	int events = 0;
	Vector2 mouseLastPos;

	[MenuItem ("Level Editor/Editor Window")]
	static void Init () {
		LevelEditor window = 
			(LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
	}

	void OnGUI () {

		if (GUILayout.Button (sceneString)) {
			sceneString = "Scene / ON";			sceneBool = true;
			waterString = "Water / OFF";		waterBool = false;
			nutrientString = "Nutrient / OFF";	nutrientBool = false;
		}
		if (GUILayout.Button (waterString)) {
			sceneString = "Scene / OFF";		sceneBool = false;
			waterString = "Water / ON";			waterBool = true;
			nutrientString = "Nutrient / OFF";	nutrientBool = false;
		}
		if (GUILayout.Button (nutrientString)) {
			sceneString = "Scene / OFF";		sceneBool = false;
			waterString = "Water / OFF";		waterBool = false;
			nutrientString = "Nutrient / ON";	nutrientBool = true;
		}
		if (GUILayout.Button ("+10 Edit Power")) {
			editFloat += 10;
		}
		if (GUILayout.Button ("-10 Edit Power")) {
			editFloat -= 10;
		}
		GUILayout.Label ("Editing Power: " + editFloat);

		GUILayout.Label ("Water: " + rayObject.GetComponent<Resourse>().water + " Nutrients: " + rayObject.GetComponent<Resourse>().nutrients);
	}

	void OnEnable () {
		SceneView.onSceneGUIDelegate += OnSceneGUI;
	}

	void OnDisable () {
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
	}

	public void OnSceneGUI (SceneView sceneview) {

		if (!sceneBool) {
			for (int i = 0; i < GameObject.Find("GM").GetComponent<Grid>().heksagons.Length; i++) {
				if (GameObject.Find("GM").GetComponent<Grid>().heksagons[i] != null) {
				GameObject.Find("GM").GetComponent<Grid>().heksagons[i].GetComponent<HexVisualizer>().VisUpdate();
				}
			}
		}
		mouseLastPos = Event.current.mousePosition;

		if (Camera.current) {
			Ray camRay = HandleUtility.GUIPointToWorldRay (mouseLastPos);
			RaycastHit hitPoint;
			
			if (Physics.Raycast (camRay, out hitPoint, 100f)) {

				Repaint();
				Resourse r = hitPoint.collider.gameObject.GetComponent<Resourse>();
				if (r) {
					rayObject = hitPoint.collider.gameObject;

					if (Event.current.Equals(Event.KeyboardEvent("E")) && !sceneBool) {
						if (waterBool) {
							r.water = editFloat;
						}
						if (nutrientBool) {
							r.nutrients = editFloat;
						}
					}
				}
			}
		}
		events++; //Debug.Log(mouseLastPos);
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
