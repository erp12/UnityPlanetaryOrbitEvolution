using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : MonoBehaviour {
	//public static float G = 0.00006673f; // actually 6.673×10^−11 N·(m/kg)^2
	public static float G = 0.006673f;
	public static int lastID;

	public static int InitialPosMag = 10;	// in km from (0, 0, 0)
	public static int InitialVelMag = 10;	// in km/frame
	public static int MaxMass = 1000;		// in kg
	private static int density = 2000;		// in kg/m^3	5510 = earth	1408 = sun

	public GameObject planetPrefab;
	public static List<GameObject> planets = new List<GameObject>();
 
	public static int GetNextID() {
		lastID++;
		return lastID;
	}

	public static Vector3 MassToScale(float mass) {
		float d = mass/density;
		return new Vector3(d, d, d);
	}

	public static Color RandomDistinctColor() {
		switch((int)Random.Range(0, 6)) {
			case 0 : return Color.red;
				break;
			case 1 : return Color.blue;
				break;
			case 2 : return Color.green;
				break;
			case 3 : return Color.yellow;
				break;
			case 4 : return Color.cyan;
				break;
			case 5 : return Color.magenta;
				break;
			default : return Color.white;
		}	
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnPlanet", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnPlanet(){
		GameObject newPlanet = Instantiate(planetPrefab) as GameObject;
		planets.Add(newPlanet);
	}
}
