using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

	public GameObject gm;

	GameObject wind;
	GameObject life;
	GameObject music;

	float lifeRatio;

	void Start () {

		wind = transform.FindChild("wind").gameObject;
		life = transform.FindChild ("life").gameObject;
		music = transform.FindChild ("music").gameObject;

	}

	void Update () {

		lifeRatio = (float)gm.GetComponent<LifeToHex> ().lifeToHexRatio;

		if (lifeRatio < 10) {
			wind.GetComponent<AudioSource>().volume = 1;
			life.GetComponent<AudioSource>().volume = 0;
			music.GetComponent<AudioSource>().volume = 0;
		}

		if (lifeRatio > 10 && lifeRatio < 50) {
			wind.GetComponent<AudioSource>().volume = 0;
			life.GetComponent<AudioSource>().volume = 1;
			music.GetComponent<AudioSource>().volume = 0;
		}

		if (lifeRatio > 50) {
			wind.GetComponent<AudioSource>().volume = 0;
			life.GetComponent<AudioSource>().volume = 0;
			music.GetComponent<AudioSource>().volume = 1;
		}
	}
}
