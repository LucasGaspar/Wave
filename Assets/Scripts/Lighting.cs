using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lighting : MonoBehaviour {

	public Color sunrise;
	public Color midday;
	public Color sunset;
	public Light nightLight;

	int hour = 0;

	// Use this for initialization
	void Start () {
		transform.position = Vector3.zero;
		hour = System.DateTime.Now.Hour;
		hour = 22;
		int angle = hour * 15;
		angle -= 90;
		gameObject.transform.localEulerAngles = new Vector3 (angle, 0, 0);

		if (hour >= 17) {
			GetComponent<Light> ().intensity = 0.5f;
			GetComponent<Light> ().color = sunset;
		}
	
		if (hour <= 8) {
			GetComponent<Light> ().intensity = 0.5f;
			GetComponent<Light> ().color = sunrise;
		}

		if (hour >= 18 || hour < 7) {
			GetComponent<Light> ().intensity = 0.1f;
			nightLight.enabled = true;
		}
	}

}
