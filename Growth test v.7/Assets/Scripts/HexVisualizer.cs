using UnityEngine;
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

	void Start () {

		GetComponent<Renderer>().material.shaderKeywords = new string[1]{"_NORMALMAP"};

		VisUpdate();
		SpawnResourceFlowers ();
		ResFlowerUpdate ();
	}

	void Update () {

		timer += Time.deltaTime;
		if (timer > timeToUpdate){
			timer -= timeToUpdate;
			VisUpdate();
			ResFlowerUpdate();
		}
	}

	void SpawnResourceFlowers () {

		wflowers = new GameObject[5];
		nflowers = new GameObject[5];

		//Water on the left
		float[] wX = {-0.6f,-0.5f,-0.4f,-0.3f,-0.2f};
		float[] wZ = {0,-0.2f,0.15f,-0.2f,0f};

		//Nutrient on the right
		float[] nX = {0.6f,0.5f,0.4f,0.3f,0.2f};
		float[] nZ = {0,-0.2f,0.15f,-0.2f,0f};

		GameObject holder = (GameObject)Instantiate (resFlowerHolder, transform.position, new Quaternion (0,0,0,0));
		holder.transform.parent = transform;

		for (int i = 0; i < 5; i++) {

			float rF = Random.Range(-0.08f, 0.08f);
																										//Y still as "Magic number"
			GameObject wFlowerIns = (GameObject)Instantiate (waterFlower, transform.position + new Vector3 (wX[i]+rF,0.3f,wZ[i]+rF) , new Quaternion (0,0,0,0));
			GameObject nFlowerIns = (GameObject)Instantiate (nutrientFlower, transform.position + new Vector3 (nX[i]+rF,0.3f,nZ[i]+rF) , new Quaternion (0,0,0,0));

			wFlowerIns.transform.parent = holder.transform; wFlowerIns.SetActive (false);
			nFlowerIns.transform.parent = holder.transform; nFlowerIns.SetActive (false);

			wflowers[i] = wFlowerIns;
			nflowers[i] = nFlowerIns;
		}
	}

	void ResFlowerUpdate () {

		nutrientIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().nutrients) / 20;
		waterIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().water) / 20;

		for (int i = 0; i < waterIndex; i++) {
			wflowers[i].SetActive(true);
		}
		for (int i = 0; i < nutrientIndex; i++) {
			nflowers[i].SetActive(true);
		}
	}



	public void VisUpdate () {

		nutrientIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().nutrients) / 25;
		waterIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().water) / 25;


		GetComponent<Renderer>().sharedMaterial.SetTexture ("_MainTex",nutrientTexture [nutrientIndex]);
		GetComponent<Renderer> ().sharedMaterial.SetTexture ("_BumpMap", normalMap);
//		GetComponent<Renderer> ().material.SetFloat ("_BumpScale", normalMapLevel[nutrientIndex]);

		GetComponent<Renderer>().sharedMaterial.color = waterColor [waterIndex];
	}
}