using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	public Material white;
	GameObject[] life;
	GameObject[] hex;

	void Update () {

		//R for Reset
		if (Input.GetKeyDown(KeyCode.R)) {
			life = GameObject.FindGameObjectsWithTag("Life");
			hex = GameObject.FindGameObjectsWithTag("Hex");

//			Reset for hex color
			for (int i = 0; i <= hex.Length-1; i++) {
				hex[i].GetComponent<Renderer>().material = white;
				hex[i].GetComponent<Resourse>().lifeCanGrow = false;
				hex[i].GetComponent<Resourse>().lifeSpawned = false;
			}
//			Destroys all life
			for (int i = 0; i <= life.Length-1; i++){
				Destroy (life[i]);
			}
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			hex = GameObject.FindGameObjectsWithTag("Hex");

			for (int i = 0; i <= hex.Length-1; i++) {
				if (hex[i].GetComponent<Resourse>().lifeCanGrow != false) {
					Debug.Log ("RESET SCRIPT B | Can grow: " + i);
				}
				if (hex[i].GetComponent<Resourse>().lifeSpawned != false) {
					Debug.Log ("RESET SCRIPT B | Spawned: " + i);
				}
			}
		}
	}
}
