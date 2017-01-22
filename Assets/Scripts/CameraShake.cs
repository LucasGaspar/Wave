using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	static CameraShake instance;

	void Start ()
	{
		instance = this;
	}
	

	public static void Shake ()
	{
		//iTween.MoveTo(gameObject, hash);
		iTween.ShakePosition(instance.gameObject, new Vector3(0.05f,0.05f,0.05f), 0.1f);
	}

	public static void MeteorShake ()
	{
		//iTween.MoveTo(gameObject, hash);
		iTween.ShakePosition(instance.gameObject, new Vector3(0.5f,0.5f,0.5f), 1f);
	}
}
