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

		division = timeToLose / 8;
		imageIndex = (int)Mathf.Round(timer / division);
		imageIndex = Mathf.Clamp (imageIndex, 0, 8);

		GetComponent<Image>().sprite = timerImageList [imageIndex];


	}
}
