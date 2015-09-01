using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour {
	

	public float timeToLose = 30;
	float scaleByTime;
	float timer;
	Text text;
	GameObject timerImage;
	
	void Awake () {
		timerImage = GameObject.Find("TimerImage");
		timer = timeToLose;
		text = GetComponent <Text> ();
	}
	
	void Update () {
		
		timer -= Time.deltaTime;
		float newTimer = Mathf.Round (timer);
		scaleByTime = timer / timeToLose;

		if (newTimer <= 0) {
			timer = 0;
			timerImage.SetActive(false);
			text.text = "Time's Up!";
		} else {

		text.text = "Timer: " + newTimer;
		timerImage.transform.localScale = new Vector3(timerImage.transform.localScale.x, scaleByTime);

		}
	}
}
