using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour {

	public static List <Plant> allThePlants = new List<Plant> ();
	
	public enum plantState {sprouting, adult, flower};
	
	public float nutrientDrain = 10;
	public int nutrientDrainRadius = 1;
	
	public float nutrientProduktion = 10;
	public int nutrientProduktionRadius = 1;
	public float waterProduktion = 10;
	public int waterProduktionRadius = 1;
	
	public float waterBind = 10;
	public int waterBindRadius = 1;
	
	public float requiredTimeToGrow = 10;
	public float requiredTimeToFlower = 10;
	public float requiredTimeToSeed = 10;
	public float requiredTimeToDie = 10;

	public int seedMax = 2;
	public int seedMin = 0;
	public int seedIndex = 0;

	public float scaleOffSetPercent = 10f;
	
	//
	
	public bool waterBindIsRelase = true;
	public bool waterRadiusBindIsRelase = true;
	
	public bool nutrientBindIsRelase = true;
	public bool nutrientRadiusBindIsRelase = true;
	
	public bool nutrientNeedIsDrain = true;
	public bool nutrientRadiusNeedIsDrain = true;
	public bool waterNeedIsDrain = true;
	public bool waterRadiusNeedIsDrain = true;
	
	public float waterRelease = 10;
	public int waterReleaseRadius = 1;
	
	public float nutrientNeed = 10;
	public int nutrientNeedRadius = 1;
	public float waterNeed = 10;
	public int waterNeedRadius = 1;
	
	public float waterDrain = 0;
	public int waterDrainRadius = 0;

	public float nutrientBind = 0;
	public int nutrientBindRadius = 0;
	
	public float nutrientRelease = 0;
	public int nutrientReleaseRadius = 0;
	
	public float nutrientOverSaturation = 100;
	public int nutrientOverSaturationRadius = 0;
	public float waterOverSaturation = 100;
	public int waterOverSaturationRadius = 0;

	plantState currentPlantState = plantState.sprouting;
	float timer = 0;
	float deathTimer = 0;
	GameObject parentHex;
	float requiredTime;
	Animator anim;
	int xPos;
	int yPos;
	bool isDying;
	int myIndex;
	float resourceDeltaTime;

	GameObject lushPlant;
	GameObject wiltedPlant;
	GameObject wiltedSapling;
	ParticleSystem particle;
	
	void RemoveEmpty (List <GameObject> hexesTemp, bool onNutrients) {
		if (onNutrients) {
			for (int i = hexesTemp.Count - 1; i >= 0; i --) {
				if (hexesTemp [i].GetComponent<Resourse> ().nutrients <= 0) {
					hexesTemp.RemoveAt (i);
				}
			}
		} else {
			for (int i = hexesTemp.Count - 1; i >= 0; i --) {
				if (hexesTemp [i].GetComponent<Resourse> ().water <= 0) {
					hexesTemp.RemoveAt (i);
				}
			}
		}
	}
	
	float GetLeast (List <GameObject> hexesTemp, bool onNutrients) {
		float least = Mathf.Infinity;
		if (onNutrients) {
			for (int i = 0; i < hexesTemp.Count; i ++) {
				if (least > hexesTemp [i].GetComponent<Resourse> ().nutrients) {
					least = hexesTemp [i].GetComponent<Resourse> ().nutrients;
				}
			}
		} else {
			for (int i = 0; i < hexesTemp.Count; i ++) {
				if (least > hexesTemp [i].GetComponent<Resourse> ().water) {
					least = hexesTemp [i].GetComponent<Resourse> ().water;
				}
			}
		}
		return least;
	}

	void DecreaseIndex () {
		myIndex --;
	}

	public void SetVariablesForUpdate () {
		isDying = false;
		resourceDeltaTime = Time.deltaTime;
		if (currentPlantState != plantState.sprouting || timer < requiredTime)
			timer += Time.deltaTime;
	}

	public void SoilCheck () {
		if (currentPlantState != plantState.sprouting) {
			float totalWaterNeed = waterNeed * HF.RadiusHexCount(waterNeedRadius);
			List <GameObject> hexesTemp = HF.HexesList (waterNeedRadius, xPos, yPos);
			float totalWater = 0;
			for (int i = 0; i < hexesTemp.Count; i ++) {
				totalWater += hexesTemp[i].GetComponent<Resourse> ().water;
			}
			float totalNutrientNeed = nutrientNeed * HF.RadiusHexCount(nutrientNeedRadius);
			hexesTemp = HF.HexesList (nutrientNeedRadius, xPos, yPos);
			float totalNutrients = 0;
			for (int i = 0; i < hexesTemp.Count; i ++) {
				totalNutrients += hexesTemp[i].GetComponent<Resourse> ().nutrients;
			}
			float totalWaterOverSaturation = waterOverSaturation * HF.RadiusHexCount(waterOverSaturationRadius);
			hexesTemp = HF.HexesList (waterOverSaturationRadius, xPos, yPos);
			float totalWaterForOverSaturation = 0;
			for (int i = 0; i < hexesTemp.Count; i ++) {
				totalWaterForOverSaturation += hexesTemp[i].GetComponent<Resourse> ().water;
			}
			float totalNutrientOverSaturation = nutrientOverSaturation * HF.RadiusHexCount(nutrientOverSaturationRadius);
			hexesTemp = HF.HexesList (nutrientOverSaturationRadius, xPos, yPos);
			float totalNutrientsForOverSaturation = 0;
			for (int i = 0; i < hexesTemp.Count; i ++) {
				totalNutrientsForOverSaturation += hexesTemp[i].GetComponent<Resourse> ().nutrients;
			}
			if (totalWater < totalWaterNeed || totalNutrients < totalNutrientNeed || totalWaterForOverSaturation > totalWaterOverSaturation || totalNutrientsForOverSaturation > totalNutrientOverSaturation) {
				isDying = true;
			}
		}
	}
	
	public void Bind () {
		if (currentPlantState == plantState.sprouting && timer >= requiredTime) {
			float totalWaterBind = waterBind * HF.RadiusHexCount (waterBindRadius);
			List <GameObject> hexesTemp = HF.HexesList (waterBindRadius, xPos, yPos);
			float totalWater = 0;
			for (int i = 0; i < hexesTemp.Count; i ++) {
				totalWater += hexesTemp [i].GetComponent<Resourse> ().water;
			}
			float totalNutrientBind = nutrientBind * HF.RadiusHexCount (nutrientBindRadius);
			hexesTemp = HF.HexesList (nutrientBindRadius, xPos, yPos);
			float totalNutrients = 0;
			for (int i = 0; i < hexesTemp.Count; i ++) {
				totalNutrients += hexesTemp [i].GetComponent<Resourse> ().nutrients;
			}
			if (totalWater >= totalWaterBind && totalNutrients >= totalNutrientBind) {
				bool b = true;
				hexesTemp = HF.HexesList (waterBindRadius, xPos, yPos);
				while (b && hexesTemp.Count > 0) {
					RemoveEmpty (hexesTemp, false);
					float least = GetLeast (hexesTemp, false);
					float bindage = totalWaterBind / hexesTemp.Count;
					if (least < bindage) {
						bindage = least;
					} else {
						b = false;
					}
					for (int i = 0; i < hexesTemp.Count; i ++) {
						hexesTemp [i].GetComponent<Resourse> ().water -= bindage;
						totalWaterBind -= bindage;
					}
				}
				b = true;
				hexesTemp = HF.HexesList (nutrientBindRadius, xPos, yPos);
				while (b && hexesTemp.Count > 0) {
					RemoveEmpty (hexesTemp, true);
					float least = GetLeast (hexesTemp, true);
					float bindage = totalNutrientBind / hexesTemp.Count;
					if (least < bindage) {
						bindage = least;
					} else {
						b = false;
					}
					for (int i = 0; i < hexesTemp.Count; i ++) {
						hexesTemp [i].GetComponent<Resourse> ().nutrients -= bindage;
						totalNutrientBind -= bindage;
					}
				}
			} else {
				isDying = true;
			}
		}
	}

	public void CircleOfLife () {
		if (isDying) {
			if (lushPlant && lushPlant.activeSelf)
				lushPlant.SetActive (false);
			deathTimer += Time.deltaTime;
			if (currentPlantState == plantState.sprouting) {
				timer = requiredTime;
				if (wiltedSapling && !wiltedSapling.activeSelf)
					wiltedSapling.SetActive (true);
			} else {
				timer = 0;
				if (currentPlantState == plantState.flower) {
					anim.SetTrigger ("testTrigger");
					currentPlantState = plantState.adult;
					requiredTime = requiredTimeToFlower;
				}
				if (wiltedPlant && !wiltedPlant.activeSelf)
					wiltedPlant.SetActive (true);
				if (deathTimer >= requiredTimeToDie) {
					List <GameObject> hexesTemp = HF.HexesList (waterReleaseRadius, xPos, yPos);
					for (int i = 0; i < hexesTemp.Count; i ++) {
						hexesTemp [i].GetComponent<Resourse> ().water += waterRelease;
					}
					hexesTemp = HF.HexesList (nutrientReleaseRadius, xPos, yPos);
					for (int i = 0; i < hexesTemp.Count; i ++) {
						hexesTemp [i].GetComponent<Resourse> ().nutrients += nutrientRelease;
					}
				}
			}
		} else {
			if (lushPlant && !lushPlant.activeSelf)
				lushPlant.SetActive (true);
			if (wiltedPlant && wiltedPlant.activeSelf)
				wiltedPlant.SetActive (false);
			if (wiltedSapling && wiltedSapling.activeSelf)
				wiltedSapling.SetActive (false);
			deathTimer = 0;
			if (timer >= requiredTime) {
				timer -= requiredTime;
				if (currentPlantState == plantState.sprouting)
					resourceDeltaTime = timer;
				anim.SetTrigger ("testTrigger");
				if(currentPlantState == plantState.adult) {
					currentPlantState = plantState.flower;
					requiredTime = requiredTimeToSeed;
				} else {
					if (currentPlantState != plantState.sprouting) {
						GameInterFace.seeds[seedIndex] += Random.Range(seedMin, seedMax + 1);
						if(particle)
							particle.Play();

					}
					currentPlantState = plantState.adult;
					requiredTime = requiredTimeToFlower;
				}
			}
			if (currentPlantState != plantState.sprouting) {
				List <GameObject> hexesTemp = HF.HexesList (waterProduktionRadius, xPos, yPos);
				for (int i = 0; i < hexesTemp.Count; i ++) {
					hexesTemp [i].GetComponent<Resourse> ().water += resourceDeltaTime * waterProduktion;
				}
				hexesTemp = HF.HexesList (nutrientProduktionRadius, xPos, yPos);
				for (int i = 0; i < hexesTemp.Count; i ++) {
					hexesTemp [i].GetComponent<Resourse> ().nutrients += resourceDeltaTime * nutrientProduktion;
				}
			}
		}
	}
	
	public void Drain () {
		if (currentPlantState != plantState.sprouting) {
			float totalWaterDrain = resourceDeltaTime * waterDrain * HF.RadiusHexCount (waterDrainRadius);
			float totalNutrientDrain = resourceDeltaTime * nutrientDrain * HF.RadiusHexCount (nutrientDrainRadius);
			bool b = true;
			List <GameObject> hexesTemp = HF.HexesList (waterDrainRadius, xPos, yPos);
			while (b && hexesTemp.Count > 0) {
				RemoveEmpty (hexesTemp, false);
				float least = GetLeast (hexesTemp, false);
				float drainage = totalWaterDrain / hexesTemp.Count;
				if (least < drainage) {
					drainage = least;
				} else {
					b = false;
				}
				for (int i = 0; i < hexesTemp.Count; i ++) {
					hexesTemp [i].GetComponent<Resourse> ().water -= drainage;
					totalWaterDrain -= drainage;
				}
			}
			b = true;
			hexesTemp = HF.HexesList (nutrientDrainRadius, xPos, yPos);
			while (b && hexesTemp.Count > 0) {
				RemoveEmpty (hexesTemp, true);
				float least = GetLeast (hexesTemp, true);
				float drainage = totalNutrientDrain / hexesTemp.Count;
				if (least < drainage) {
					drainage = least;
				} else {
					b = false;
				}
				for (int i = 0; i < hexesTemp.Count; i ++) {
					hexesTemp [i].GetComponent<Resourse> ().nutrients -= drainage;
					totalNutrientDrain -= drainage;
				}
			}
		}
	}

	public void Death () {
		if (deathTimer >= requiredTimeToDie) {
			allThePlants.RemoveAt (myIndex);
			for (int i = myIndex; i < allThePlants.Count; i++) {
				allThePlants[i].DecreaseIndex();
			}
			myIndex = -1;
			Destroy (gameObject);
		}
	}
	
	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (0, Random.Range (0f, 360f), 0);
		transform.localScale = transform.localScale * (100 + Random.Range((0 - scaleOffSetPercent), scaleOffSetPercent)) / 100;
		myIndex = allThePlants.Count;
		allThePlants.Add (this);
		requiredTime = requiredTimeToGrow;
		parentHex = transform.parent.gameObject;
		anim = GetComponentInChildren<Animator> ();
		xPos = parentHex.GetComponent<Resourse> ().xPos;
		yPos = parentHex.GetComponent<Resourse> ().yPos;

		lushPlant = transform.Find ("plant").gameObject;
		wiltedPlant = transform.Find ("plant_wilted").gameObject;
		wiltedSapling = transform.Find ("sapling_wilted").gameObject;
		particle = transform.Find ("particle").gameObject.GetComponent<ParticleSystem>();


		if (lushPlant)
			lushPlant.SetActive (true);
		if (wiltedPlant)
			wiltedPlant.SetActive (false);
		if (wiltedSapling)
			wiltedSapling.SetActive (false);
		
		if (waterBindIsRelase)
			waterRelease = waterBind;
		if (waterRadiusBindIsRelase)
			waterReleaseRadius = waterBindRadius;
		
		if (nutrientBindIsRelase)
			nutrientRelease = nutrientBind;
		if (nutrientRadiusBindIsRelase)
			nutrientReleaseRadius = nutrientBindRadius;
		
		if (waterNeedIsDrain) {
			if (waterDrain > 0) {
				waterNeed = Mathf.Epsilon;
			} else {
				waterNeed = 0;
			}
		}
		if (waterRadiusNeedIsDrain)
			waterNeedRadius = waterDrainRadius;
		if (nutrientNeedIsDrain) {
			if (nutrientDrain > 0) {
				nutrientNeed = Mathf.Epsilon;
			} else {
				nutrientNeed = 0;
			}
		}
		if (nutrientRadiusNeedIsDrain)
			nutrientNeedRadius = nutrientDrainRadius;
	}

	void OnDestroy () {
		if (myIndex != -1) {
			allThePlants.RemoveAt (myIndex);
			for (int i = myIndex; i < allThePlants.Count; i++) {
				allThePlants[i].DecreaseIndex();
			}
		}
	}
}
