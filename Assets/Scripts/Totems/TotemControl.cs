﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TotemControl : MonoBehaviour {

    static int AliveTotems = 0;
    public static UnityAction OnLastTotem;

    [SerializeField]    float jumpTime = .5f;
    [SerializeField]    Vector3 jumpVector = Vector3.up * 15;
    [SerializeField]    Vector3 initForce = Vector3.up *50;
    [SerializeField]    Vector3 deadForce = Vector3.up  * 30;
    public string buttonName = "4";

    [Header("References")]
	[SerializeField] public Hexagon hexagon;
	[SerializeField] public GameObject[] skins;
	[SerializeField] public AudioClip jumpSound;
	[SerializeField] public AudioClip landSound;
	[SerializeField] public AudioClip dieSound;
    Rigidbody rigidbody;
    ParticleSystem particles;
    [Header("Debug")]
	int currentSkin = 0;
    [SerializeField]    float timer = 0;
    [SerializeField]    bool jumpButtonPressed;
    [SerializeField]    bool jumping;
    [SerializeField]    bool isGrounded;
    [SerializeField]    bool canWave = false;

	public TextMesh myScore;
	public GameObject crown;

	// Use this for initialization
	void Awake () {
        rigidbody = GetComponent<Rigidbody>();
        particles = GetComponentInChildren<ParticleSystem>( );
        StartUpCounter.OnStart += SetWave;
        AliveTotems++;
		ChangeSkin();
    }

	void Start(){
		if (ScoreManager.SM.firstPlay) {
			switch (buttonName) {
			case "1":
				myScore.text = "W";
				break;
			case "2":
				myScore.text = "A";
				break;
			case "3":
				myScore.text = "Z";
				break;
			case "4":
				myScore.text = "M";
				break;
			case "5":
				myScore.text = "K";
				break;
			case "6":
				myScore.text = "I";
				break;
			}
			//ScoreManager.SM.firstPlay = false;
		} else {
			switch (buttonName) {
			case "1":
				myScore.text = ScoreManager.SM.p1Score.ToString();
				break;
			case "2":
				myScore.text = ScoreManager.SM.p2Score.ToString();
				break;
			case "3":
				myScore.text = ScoreManager.SM.p3Score.ToString();
				break;
			case "4":
				myScore.text = ScoreManager.SM.p4Score.ToString();
				break;
			case "5":
				myScore.text = ScoreManager.SM.p5Score.ToString();
				break;
			case "6":
				myScore.text = ScoreManager.SM.p6Score.ToString();
				break;
			}
		}


		ScoreManager.SM.UpdateKing ();
		if (buttonName == ScoreManager.SM.currentKing) {
			crown.SetActive (true);
		}

		Invoke ("DisableScore", 5);
	}

	void DisableScore(){
		myScore.text = "";
	}

    void OnDestroy()
    {
		
        StartUpCounter.OnStart -= SetWave;
        AliveTotems--;
        if( AliveTotems <= 1 )
        {
            if(OnLastTotem != null)
                 OnLastTotem( );
        }
            

    }

    void SetWave()
    {
        canWave = true;
    }


    // Update is called once per frame
    void Update () {
        jumpButtonPressed =  Input.GetButton(buttonName);
        if(transform.position.y < .15f &&  hexagon.transform.localPosition.y > .15f )
        {
            Dead( );
        }
	}

    void FixedUpdate()
    {
        if(isGrounded && jumpButtonPressed && !jumping)
        {
            rigidbody.velocity = Vector2.zero;
            timer = 0;
            jumping = true;
            isGrounded = false;
            rigidbody.AddForce(initForce,ForceMode.Impulse);
			iTween.ScaleTo(gameObject, new Vector3(1,1.1f,1), 1f);
			AudioManager.ReproduceSound(jumpSound);

			if(!canWave)
				ChangeSkin();
        }

        if(jumping && jumpButtonPressed )
        {
            if (timer < jumpTime)
            {
                float proportionCompleted = timer / jumpTime;
                Vector2 thisFrameJumpVector = Vector2.Lerp(jumpVector, Vector3.zero, proportionCompleted);
                rigidbody.AddForce(thisFrameJumpVector);
                timer += Time.deltaTime;
            }
            else
            {
                jumping = false;
				iTween.ScaleTo(gameObject, new Vector3(1,1,1), 1f);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
		if(hexagon && !isGrounded)
		{
            if( canWave )
                hexagon.Wave( );
			particles.Play( );
			this.transform.localScale = Vector3.one;
			iTween.ScaleFrom(gameObject, new Vector3(1,0.75f,1), 0.5f);
		}
		AudioManager.ReproduceSound(landSound);
        isGrounded = true;
        jumping = false;
    }


    private void Dead()
    {
        rigidbody.AddForce( deadForce, ForceMode.Impulse );
		AudioManager.ReproduceSound(dieSound);
        Destroy( this );
    }

	int SelectNewSkin()
	{
		if(skins.Length <= 1)
		{
			return 0;
		}

		int newSkin;
		do{newSkin = Random.Range(0,skins.Length);}
		while(newSkin == currentSkin);

		return newSkin;
	}

	void ChangeSkin()
	{
		skins[currentSkin].SetActive(false);
		currentSkin = SelectNewSkin();
		skins[currentSkin].SetActive(true);
	}
}
