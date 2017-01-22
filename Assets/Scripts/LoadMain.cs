using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMain : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Invoke( "LoadNext", 11 );
    }

    void LoadNext()
    {
        Application.LoadLevel( "Game" );
    }
	

}
