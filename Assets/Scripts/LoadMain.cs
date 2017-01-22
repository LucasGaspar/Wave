using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMain : MonoBehaviour {

    public string sceneName = "Game";
    public float timer =11;
	// Use this for initialization
	void Start () {

        Invoke( "LoadNext", timer);
    }

    void LoadNext()
    {
        Application.LoadLevel( sceneName);
    }
	

}
