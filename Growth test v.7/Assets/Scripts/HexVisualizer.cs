﻿using UnityEngine;
using System.Collections;

public class HexVisualizer : MonoBehaviour {

	public GameObject waterFlower;
	public GameObject nutrientFlower;
	public GameObject resFlowerHolder;

	GameObject[] wflowers;
	GameObject[] nflowers;

	float timer;
	public float timeToUpdate;

	float[] normalMapLevel = {0,0.1f,0.2f,0.6f,0.9f};
	public Texture normalMap;
	public Texture[] nutrientTexture;
	public Color[] waterColor;

	int nutrientIndex;
	int waterIndex;

	public bool showLifeTimer;
	bool showFlowers;
	bool startFlowerTimer;
	float flowerTimer;

	float randomTimer;
	float randomTime;

	void Start () {

		GetComponent<Renderer>().material.shaderKeywords = new string[1]{"_NORMALMAP"};

		VisUpdate();
		SpawnResourceFlowers ();
//		ResFlowerUpdate ();
		randomTime = Random.Range (0,3f);
	}

	void Update () {

		timer += Time.deltaTime;
		if (timer > timeToUpdate){
			timer -= timeToUpdate;
			VisUpdate();
			ResFlowerUpdate();
		}

		if (showLifeTimer == true) {
			randomTimer += Time.deltaTime;
			if (randomTimer > randomTime) {
				transform.FindChild("Life").gameObject.SetActive (true);
				showLifeTimer = false;
				randomTimer = 0;
			}
		}

		if (startFlowerTimer) {
			flowerTimer += Time.deltaTime;
			if (flowerTimer > 7 + randomTime) {
				startFlowerTimer = false;
				showFlowers = true;
				flowerTimer = 0;
			}
		}
	}

	void SpawnResourceFlowers () {

		wflowers = new GameObject[5];
		nflowers = new GameObject[5];

		//WaterFlower position on the left of the hex
		float[] wX = {-0.2f,-0.55f,-0.7f,-0.55f,-0.2f};
		float[] wZ = {-0.6f,-0.4f,0f,0.4f,0.6f}; //2D Y

		//NutrientFlower position on the right of the hex
		float[] nX = {0.2f,0.55f,0.7f,0.55f,0.2f};
		float[] nZ = {-0.6f,-0.4f,0f,0.4f,0.6f}; //2D Y

		GameObject holder = (GameObject)Instantiate (resFlowerHolder, transform.position, new Quaternion (0,0,0,0));
		holder.transform.parent = transform;

		for (int i = 0; i < 5; i++) {

			float rF = Random.Range(-0.08f, 0.08f);

			GameObject wFlowerIns = (GameObject)Instantiate (waterFlower, transform.position + new Vector3 (wX[i]+rF,0,wZ[i]+rF) , new Quaternion (0,0,0,0));
			GameObject nFlowerIns = (GameObject)Instantiate (nutrientFlower, transform.position + new Vector3 (nX[i]+rF,0,nZ[i]+rF) , new Quaternion (0,0,0,0));

			wFlowerIns.transform.parent = holder.transform; wFlowerIns.SetActive (false);
			nFlowerIns.transform.parent = holder.transform; nFlowerIns.SetActive (false);

			wflowers[i] = wFlowerIns;
			nflowers[i] = nFlowerIns;
		}
	}

	public void ResFlowerUpdate () {

		nutrientIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().nutrients) / 20;
		waterIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().water) / 20;

		if (waterIndex != wflowers.Length || nutrientIndex != nflowers.Length) {
			ResFlowerReset();
		}

		if (transform.FindChild ("Life")) {
			startFlowerTimer = true;
			if (showFlowers) {
				for (int i = 0; i < waterIndex; i++) {
					wflowers [i].SetActive (true);
				}
				for (int i = 0; i < nutrientIndex; i++) {
					nflowers [i].SetActive (true);
				}
			} 
		} else {
			showFlowers = false;
			ResFlowerHardReset ();
		}
	}

	public void ResFlowerReset () {

		nutrientIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().nutrients) / 20;
		waterIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().water) / 20;

		for (int i = 0; i < 5 - waterIndex; i++) {
			wflowers [4-i].SetActive (false);
		}
		for (int i = 0; i < 5 - nutrientIndex; i++) {
			nflowers [4-i].SetActive (false);
		}
	}

	//"Debug"
	void ResFlowerHardReset () {

		for (int i = 0; i < wflowers.Length; i++) {
			wflowers [i].SetActive (false);
		}
		for (int i = 0; i < nflowers.Length; i++) {
			nflowers [i].SetActive (false);
		}
	}

	public void VisUpdate () {

		nutrientIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().nutrients) / 25;
		waterIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().water) / 25;


		GetComponent<Renderer>().material.SetTexture ("_MainTex",nutrientTexture [nutrientIndex]);
		GetComponent<Renderer> ().sharedMaterial.SetTexture ("_BumpMap", normalMap);
		GetComponent<Renderer> ().sharedMaterial.SetFloat ("_BumpScale", normalMapLevel[nutrientIndex]);

		GetComponent<Renderer>().material.color = waterColor [waterIndex];
	}
}