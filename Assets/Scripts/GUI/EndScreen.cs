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
		AudioManager.SetMusicVolume(0.25f);

        if(showWinner)
        {
			GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ().UpdateScores ();
            winnerText.gameObject.SetActive( true );
            Invoke( "Restart", 4 );
            showWinner = false;
            audioSource.clip = winSound;
            audioSource.Play( );
        }
        else
        {
            winnerText.text = "Draw!";

            audioSource.clip = loseClip;
            audioSource.Play( );
        }

    }

    private void Restart()
    {
        Application.LoadLevel( Application.loadedLevel );
    }
}
