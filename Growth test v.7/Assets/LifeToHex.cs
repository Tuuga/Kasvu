﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeToHex : MonoBehaviour {

	float numberOfHexes;
	float numberOfLife;
	float lifeToHexRatio;
	GameObject[] hexes;
	public float lifeToWin;
	public Text lifeRatio;

	bool won;

	float timer;

	void Start () {
		hexes = GetComponent<Grid> ().heksagons;
		CountLifeToHexRatio ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer > 1f) {
			CountLifeToHexRatio();
			timer = 0;
		}
		if (lifeToHexRatio >= lifeToWin) {
			WinState();
		}
	}
	void WinState () {

		if (!won) {
			Debug.Log ("Life at: " + lifeToHexRatio + "%   You Win!");
			won = true;
		}
	}

	void CountLifeToHexRatio() {

		numberOfHexes = 0;
		numberOfLife = 0;
		lifeToHexRatio = 0;

		for (int i = 0; i < hexes.Length; i++) {
			if (hexes[i] != null) {
				numberOfHexes++;

				if (hexes[i].transform.FindChild("Life") != null) {
					numberOfLife++;
				}
			}
		}

		lifeToHexRatio = numberOfLife / numberOfHexes * 100;
		lifeRatio.text = lifeToHexRatio + "%";

	}
}
