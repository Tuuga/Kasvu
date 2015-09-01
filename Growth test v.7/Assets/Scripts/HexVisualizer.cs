using UnityEngine;
using System.Collections;

public class HexVisualizer : MonoBehaviour {

	public GameObject waterFlower;
	public GameObject nutrientFlower;
	public GameObject resFlowerHolder;

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
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.G)) {
			ResourceWithLife();
		}

		timer += Time.deltaTime;
		if (timer > timeToUpdate){
			timer -= timeToUpdate;
			VisUpdate();
		}
	}

	void SpawnResourceFlowers () {

		//Water on the left
		float[] wX = {-0.6f,-0.5f,-0.4f,-0.3f,-0.2f};
		float[] wZ = {0,-0.2f,0.15f,-0.2f,0f};

		//Nutrient on the right
		float[] nX = {0.6f,0.5f,0.4f,0.3f,0.2f};
		float[] nZ = {0,-0.2f,0.15f,-0.2f,0f};

		for (int i = 0; i < 5; i++) {

			GameObject wFlowerIns = (GameObject)Instantiate (waterFlower, transform.position + new Vector3 (wX[i],0.3f,wZ[i]) , new Quaternion (0, 0, 0, 0));
			GameObject nFlowerIns = (GameObject)Instantiate (nutrientFlower, transform.position + new Vector3 (nX[i],0.3f,nZ[i]) , new Quaternion (0, 0, 0, 0));

		}
	}







	void ResourceWithLife () {

		float rX = 0;
//		float rY = 0;
		float rZ = 0;

		nutrientIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().nutrients) / 25 + 1;
		waterIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().water) / 25 + 1;

		for (int i = 0; i < nutrientIndex; i++) {
			rX = Random.Range (0f,0.4f);
//			rY = Random.Range (0f,1f);
			rZ = Random.Range (-0.2f,0.2f);

			if (gameObject.transform.FindChild("Life") != null) {
				GameObject nutrientFlowerIns = (GameObject)Instantiate (nutrientFlower, transform.position + new Vector3 (rX,0.3f,rZ) , new Quaternion (0, 0, 0, 0));
				nutrientFlowerIns.transform.parent = gameObject.transform;
			}
		}

		for (int i = 0; i < waterIndex; i++) {
			rX = Random.Range (-0.4f,0f);
//			rY = Random.Range (0f,1f);
			rZ = Random.Range (-0.2f,0.2f);

			if (gameObject.transform.FindChild("Life") != null) {
				GameObject waterFlowerIns = (GameObject)Instantiate (waterFlower, transform.position + new Vector3 (rX,0.3f,rZ) , new Quaternion (0, 0, 0, 0));
				waterFlowerIns.transform.parent = gameObject.transform;
			}
		}
	}

	public void VisUpdate () {

		nutrientIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().nutrients) / 25;
		waterIndex = (int)Mathf.Round (gameObject.GetComponent<Resourse> ().water) / 25;


		GetComponent<Renderer>().sharedMaterial.SetTexture ("_MainTex",nutrientTexture [nutrientIndex]);
		GetComponent<Renderer> ().sharedMaterial.SetTexture ("_BumpMap", normalMap);
//		GetComponent<Renderer> ().material.SetFloat ("_BumpScale", normalMapLevel[nutrientIndex]);

		GetComponent<Renderer>().material.color = waterColor [waterIndex];
	}
}