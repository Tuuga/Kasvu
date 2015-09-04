using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

	public GameObject gm;

	public float volume;

	public float windPercent = 15;
	public float lifePercent = 10;
	public float musicPercent = 50;

	public float fade1;
	public float fade2;
	public float fade3;

	public GameObject wind;
	public GameObject life;
	public GameObject music;

	float lifeRatio;

	void Start () {

	}

	void Update () {

		lifeRatio = (float)gm.GetComponent<LifeToHex> ().lifeToHexRatio;

		if (lifeRatio < windPercent) {
			if (fade1 < 30) {
				fade1 += Time.deltaTime;
			}
			wind.GetComponent<AudioSource> ().volume = (fade1 / 30) * volume;
		} else {
			if (fade1 > 0) {
				fade1 -= Time.deltaTime;
			}
			wind.GetComponent<AudioSource> ().volume = (fade1 / 30) * volume;
		}

		if (lifeRatio > lifePercent && lifeRatio < musicPercent) {
			if (fade2 < 30) {
				fade2 += Time.deltaTime;
			}
			life.GetComponent<AudioSource> ().volume = (fade2 / 30) * volume;
		} else {
			if (fade2 > 10) {
				fade2 -= Time.deltaTime;
			}
			life.GetComponent<AudioSource> ().volume = (fade2 / 30) * volume;
		}

		if (lifeRatio > musicPercent) {
			if (fade3 < 30) {
				fade3 += Time.deltaTime;
			}
			music.GetComponent<AudioSource> ().volume = (fade3 / 30) * volume;
		} else {
			if (fade3 > 0) {
				fade3 -= Time.deltaTime;
			}
			music.GetComponent<AudioSource> ().volume = (fade3 / 30) * volume;
		}
	}
}
