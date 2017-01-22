using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartUpCounter : MonoBehaviour {
    public static UnityAction OnStart;

    [SerializeField]    string startText = "Go";
    [SerializeField]    int seconds = 5;

	[SerializeField]    AudioClip countdownSound;
	[SerializeField]    AudioClip startSound;

    Text counterText;

    float currentTimer = 0;
	// Use this for initialization
	void Start () {
        counterText = GetComponentInChildren<Text>( );
	}
	
	// Update is called once per frame
	void Update () {
        currentTimer += Time.deltaTime;
        if(currentTimer >= 1f)
        {
            currentTimer = 0;
            --seconds;
            if(seconds > 0)
            {
                counterText.text = seconds.ToString( );
				AudioManager.ReproduceSound(countdownSound);
            }
            else if(seconds == 0)
            {
                currentTimer = .5f;
                counterText.text = startText;
				AudioManager.ReproduceSound(startSound);
                OnStart.Invoke( );
            }
            else
            {
                Destroy( gameObject );
            }
        }
	}
}
