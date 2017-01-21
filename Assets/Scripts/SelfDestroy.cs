using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

	public void DestroyMe(){
		Invoke ("ActuallyDestroy", 4.0f);
	}

	void ActuallyDestroy(){
		Destroy (gameObject);
	}
}
