using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	public GameObject parentHex;

	public float nutrientUse = 10;
	public bool nutrientUsedAsStockpile = true;
	public int nutrientUseRadius = 1;
	public float wateruse = 10;
	public bool waterUsedAsStockpile = true;
	public int waterUseRadius = 1;

	float timer = 0;
	public float requiredTime = 10;
	Animator anim;


	//Resurssi systeemit kommentoitu pois
	void Start () {
//		parentHex = transform.parent.gameObject;
//		parentHex.GetComponent<Resourse> ().water -= 20;

		anim = GetComponent<Animator> ();
	}

	void Update () {

//		if (parentHex.GetComponent<Resourse> ().nutrients > 0) {
//			parentHex.GetComponent<Resourse> ().nutrients -= nutrientUse * Time.deltaTime;
//		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Destroy (gameObject);
		}

		timer += Time.deltaTime;
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
		}
	}

	void OnDestroy () {
//		parentHex.GetComponent<Resourse> ().water += 20;
	}
}
