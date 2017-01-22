using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour {

    public string sceneName = "Loading";

    void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(()=> Application.LoadLevel( sceneName ));

    }
}
