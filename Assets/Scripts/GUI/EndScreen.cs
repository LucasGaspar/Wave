using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

	[SerializeField] Text winnerText;
    [SerializeField] int restartTime = 5;
    bool showWinner = true;
    void Awake()
    {
        TotemControl.OnLastTotem += ShowWinner;
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
        }
        else
        {
            winnerText.text = "Tie T_T";
        }

    }

    private void Restart()
    {
        Application.LoadLevel( Application.loadedLevel );
    }
}
