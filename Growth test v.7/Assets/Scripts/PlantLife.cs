using UnityEngine;
using System.Collections;

public class PlantLife : MonoBehaviour {

	public int lifeRadius;

	float lifeGrowTimer;
	public float timeForGrowth;
	public int maxLifeRadius;

	GameObject GM;

	void Awake () {
		GM = GameObject.Find ("GM");
	}

	void Start () {
		GM.GetComponent<GrowthManager> ().SetLifeInHex (gameObject);
	}

	void Update () {

		lifeGrowTimer += Time.deltaTime;
		if (lifeGrowTimer > timeForGrowth && lifeRadius < maxLifeRadius) {
			lifeRadius++;
			lifeGrowTimer = 0;
			GM.GetComponent<GrowthManager>().SpawnLife(gameObject);
		}
	}

	void OnDestroy() {
		if (GM != null) { //Prevents errors when stopping playmode
			GM.GetComponent<GrowthManager> ().DestroyLife (gameObject);
		}
	}
}
