using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

	[SerializeField] Text winnerText;
    [SerializeField] int restartTime = 5;
    [SerializeField]
    AudioClip winSound;
    [SerializeField]
    AudioClip loseClip;
    AudioSource audioSource;
    bool showWinner = true;
    void Awake()
    {
        TotemControl.OnLastTotem += ShowWinner;
        audioSource = GetComponent<AudioSource>( );
    }

    void OnDestroy()
    {
        TotemControl.OnLastTotem -= ShowWinner;
    }

    public void ShowWinner()
    {
        if(showWinner)
        {
            winnerText.gameObject.SetActive( true );
            Invoke( "Restart", 5 );
            showWinner = false;
            audioSource.clip = winSound;
            audioSource.Play( );
        }
        else
        {
            winnerText.text = "Tie T_T";

            audioSource.clip = loseClip;
            audioSource.Play( );
        }

    }

    private void Restart()
    {
        Application.LoadLevel( Application.loadedLevel );
    }
}
