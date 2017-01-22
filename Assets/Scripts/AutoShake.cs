using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShake: MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        iTween.ScaleBy( gameObject, iTween.Hash(
"amount", Vector3.one * 1.5f,
"looptype", iTween.LoopType.pingPong,
"time", .5f,
"easeType", iTween.EaseType.linear
) );
    }
}

