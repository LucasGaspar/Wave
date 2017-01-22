using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour {


	public GameObject meteorite;
	public Transform[] targetPoint;
	[Range(1,10)]
	public int spawnRate = 1;
	public int meteorDelayTime = 20;

	// Use this for initialization
	void Start () {
		transform.position = Vector3.zero;
		StartCoroutine (StartSpawning ());
	}

	IEnumerator StartSpawning(){
		yield return new WaitForSeconds (meteorDelayTime);
		StartCoroutine (Spawn ());
	}

	IEnumerator Spawn(){
		GameObject clone = Instantiate (meteorite) as GameObject;
		clone.GetComponent<Meteorite> ().targetPoint = targetPoint;
		yield return new WaitForSeconds (10/spawnRate);
		StartCoroutine (Spawn ());
	}
}
