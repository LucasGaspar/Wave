using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTween : MonoBehaviour {

    public Vector3 fromPosition;
    public float delay;
    public float time;
	// Use this for initialization
	void Start () {
        Hashtable hash = iTween.Hash("position",fromPosition, "time", time, "delay", delay ,"onstart", "OnComplete");
        iTween.MoveFrom( this.gameObject, hash );
	}
	

    void OnComplete()
    {
        Invoke( "Particles", .5f );
        
    }
    void Particles()
    {
        var particles =  GetComponentInChildren<ParticleSystem>(true);
        if( particles )
            particles.gameObject.SetActive( true );
    }
}
