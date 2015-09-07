using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour {
	

	public float timeToLose;
	float division;
	int imageIndex;
	float timer;
	public Sprite[] timerImageList;

	void Awake () {
		timer = timeToLose;
	}
	
	void Update () {
		
		timer -= Time.deltaTime;

		imageIndex = Mathf.Clamp((int)Mathf.Round (timer / (timeToLose / 8)), 0, 8);

		GetComponent<Image>().sprite = timerImageList [imageIndex];

		if (timer <= 0) {
			Application.LoadLevel (7);
		}
	}
}
