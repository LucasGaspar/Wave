using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartUpCounter : MonoBehaviour {
    public static UnityAction OnStart;

    [SerializeField]    string startText = "Go";
    [SerializeField]    int seconds = 5;

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
            }
            else if(seconds == 0)
            {
                currentTimer = .5f;
                counterText.text = startText;
                OnStart.Invoke( );
            }
            else
            {
                Destroy( gameObject );
            }
        }
	}
}
