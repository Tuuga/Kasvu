using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	public enum resourceUseType {limit, cap, stockpile};
	
	public static resourceUseType nutrientUseType = resourceUseType.stockpile;
	public static resourceUseType waterUseType = resourceUseType.stockpile;

	public GameObject parentHex;

	public float nutrientUse = 10;
	public int nutrientUseRadius = 1;
	public float waterUse = 10;
	public int waterUseRadius = 1;

	public float nutrientProduktion = 10;
	public int nutrientProduktionRadius = 1;
	public float waterProduktion = 10;
	public int waterProduktionRadius = 1;

	public float requiredTime = 10;

	float timer = 0;

	Animator anim;

	void Start () {
		parentHex = transform.parent.gameObject;

		if (waterUseType == resourceUseType.cap) {
			parentHex.GetComponent<Resourse> ().water -= waterUse;
		}
		if (nutrientUseType == resourceUseType.cap) {
			parentHex.GetComponent<Resourse> ().nutrients -= nutrientUse;
		}

		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (parentHex.GetComponent<Resourse> ().nutrients < nutrientUse || parentHex.GetComponent<Resourse> ().water < waterUse) {
			timer -= Mathf.Max((Time.deltaTime / 2) * ((nutrientUse - parentHex.GetComponent<Resourse> ().nutrients) / nutrientUse), 0) + Mathf.Max((Time.deltaTime / 2) * ((waterUse - parentHex.GetComponent<Resourse> ().water) / waterUse), 0);
		} else {
			timer += Time.deltaTime;
		}

		if (parentHex.GetComponent<Resourse> ().water > 0 && waterUseType == resourceUseType.stockpile) {
			parentHex.GetComponent<Resourse> ().water -= waterUse * Time.deltaTime;
		}
		if (parentHex.GetComponent<Resourse> ().nutrients > 0 && nutrientUseType == resourceUseType.stockpile) {
			parentHex.GetComponent<Resourse> ().nutrients -= nutrientUse * Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Destroy (gameObject);
		}

		if (timer >= requiredTime) {
			timer -= requiredTime;
			if(anim.GetBool("hasNutrients") == false) {
				anim.SetBool ("hasNutrients", true);
			} else if(anim.GetBool("canFlower") == false) {
				anim.SetBool ("producedSeed", false);
				anim.SetBool ("canFlower", true);
			} else {
				anim.SetBool ("canFlower", false);
				anim.SetBool ("producedSeed", true);
			}
		} else if (timer <= -10) {
			Destroy (gameObject);
		}
	}

	void OnDestroy () {
		if (waterUseType == resourceUseType.cap) {
			parentHex.GetComponent<Resourse> ().water += waterUse;
		}
		if (nutrientUseType == resourceUseType.cap) {
			parentHex.GetComponent<Resourse> ().nutrients += nutrientUse;
		}
	}
}
