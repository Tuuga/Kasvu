using UnityEngine;
using System.Collections;

public class PlantManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < Plant.allThePlants.Count; i ++)
			Plant.allThePlants[i].SetVariablesForUpdate ();
		for(int i = 0; i < Plant.allThePlants.Count; i ++)
			Plant.allThePlants[i].SoilCheck ();
		for (int i = 0; i < Plant.allThePlants.Count; i ++)
			Plant.allThePlants [i].Bind ();
		for(int i = 0; i < Plant.allThePlants.Count; i ++)
			Plant.allThePlants[i].CircleOfLife ();
		for(int i = 0; i < Plant.allThePlants.Count; i ++)
			Plant.allThePlants[i].Drain ();
		for(int i = Plant.allThePlants.Count - 1; i >= 0; i --)
			Plant.allThePlants[i].Death ();
	}
}
