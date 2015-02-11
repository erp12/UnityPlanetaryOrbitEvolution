using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour {
	public GameObject pathMarker;
	private Rigidbody rb;
	private int ID;
	private Color color;

	// Use this for initialization
	void Start () {
		ID = Globals.GetNextID();
		rb = GetComponent<Rigidbody>();
		color = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
		InvokeRepeating("DropPathPoint", 0.5f, 0.5f);
		RandInit();
	}

	void RandInit() {
		// Set random Mass
		rb.mass = Random.Range(0, Globals.MaxMass);
		transform.localScale = Globals.MassToScale(rb.mass);

		// Set random initial position and velocity
		transform.position = new Vector3(Random.Range(-Globals.InitialPosMag, Globals.InitialPosMag),
		                                 Random.Range(-Globals.InitialPosMag, Globals.InitialPosMag),
		                                 Random.Range(-Globals.InitialPosMag, Globals.InitialPosMag));
		rb.velocity = new Vector3(Random.Range(-Globals.InitialVelMag, Globals.InitialVelMag),
		                          Random.Range(-Globals.InitialVelMag, Globals.InitialVelMag),
		                          Random.Range(-Globals.InitialVelMag, Globals.InitialVelMag));

	}

	// Update is called once per frame
	void Update () {
		foreach (GameObject other in Globals.planets) {
			int IDofOther = ((Planet)other.GetComponent(typeof(Planet))).GetID();
			if (IDofOther != ID) {
				float forceMagnitude = Globals.G * ((other.rigidbody.mass * rb.mass) / Vector3.Distance(other.transform.position, transform.position));
				Vector3 forceVector = (other.transform.position - transform.position).normalized;
				rigidbody.AddForce(forceVector * forceMagnitude);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Planet")) {
			print("Planet Drestroyed with ID: " + ID);
			Destroy(this.gameObject);
		}
	}

	void DropPathPoint() {
		GameObject pp = Instantiate(pathMarker, transform.position, Quaternion.identity) as GameObject;
		pp.renderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
		pp.renderer.material.color = color;
	}

	int GetID() {
		return ID;
	}
}