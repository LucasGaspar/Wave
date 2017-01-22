using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	static CameraShake instance;
	bool meteorShake = false;

	void Start ()
	{
		instance = this;
	}

	public static void Shake ()
	{
		if(!instance.meteorShake)
			iTween.ShakePosition(instance.gameObject, new Vector3(0.05f,0.05f,0.05f), 0.1f);
	}

	public static void MeteorShake ()
	{
		float shakeDuration = 1f;

		instance.meteorShake = true;
		iTween.ShakePosition(instance.gameObject, new Vector3(0.5f,0.5f,0.5f), shakeDuration);
		instance.Invoke("MeteorShakeEnded", shakeDuration);
	}

	void MeteorShakeEnded()
	{
		meteorShake = false;
	}
}
