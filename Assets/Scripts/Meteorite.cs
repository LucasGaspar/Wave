using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {

	public float speed = 1.0F;
	public GameObject explosion;
	public Transform[] targetPoint;
	Transform spawnPoint;
	float startTime;
	float journeyLength;
	int rand = 0;
	Hexagon[] hexas;
	void Start() {
		hexas = GameObject.Find ("Generator").GetComponentsInChildren<Hexagon> ();
		spawnPoint = GameObject.Find ("SpawnPoint").transform;
		startTime = Time.time;
		rand = Random.Range (0, targetPoint.Length);
		journeyLength = Vector3.Distance(spawnPoint.position, targetPoint[rand].position);
	}
	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(spawnPoint.position, targetPoint[rand].position, fracJourney);
		transform.Rotate (Vector3.right * Time.deltaTime * 50);
	}

	void CreateWave(){
	
	}

	void OnTriggerEnter(Collider other){
		print (other.transform.name);
		if ( other.transform.tag == "target") {
			GameObject explosionClone = Instantiate (explosion, new Vector3(other.transform.position.x, 0, other.transform.position.z), Quaternion.identity) as GameObject;
			explosionClone.GetComponent<SelfDestroy> ().DestroyMe ();
			hexas [0].Wave ();
			hexas [0].Wave ();
			hexas [0].Wave ();
			CameraShake.MeteorShake();
//			Hexagon hex = other.GetComponent<Hexagon> ();
//			hex.Wave ();
			Destroy (gameObject);
		}
	}
}
