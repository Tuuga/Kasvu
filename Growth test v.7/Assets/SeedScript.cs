using UnityEngine;
using System.Collections;

public class SeedScript : MonoBehaviour {

	public float speed;

	void Update () {
		transform.position += new Vector3 (0,1,0) * speed * Time.deltaTime;
	}
}
