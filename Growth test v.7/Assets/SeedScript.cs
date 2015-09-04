using UnityEngine;
using System.Collections;

public class SeedScript : MonoBehaviour {

	public float speed;
	[HideInInspector]
	public bool isMoving = false;

	void Update () {
		if(isMoving)
			transform.position += new Vector3 (0,1,0) * speed * Time.deltaTime;
	}
}
