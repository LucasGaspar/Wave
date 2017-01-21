using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemControl : MonoBehaviour {


    

    [SerializeField]    float jumpTime = .5f;
    [SerializeField]    Vector3 jumpVector = Vector3.up * 15;
    [SerializeField]    Vector3 initForce = Vector3.up *50;

    public string buttonName = "4";

    [Header("References")]
    [SerializeField] Hexagon hexagon;
    Rigidbody rigidbody;
    [Header("Debug")]
    [SerializeField]    float timer = 0;
    [SerializeField]    bool jumpButtonPressed;
    [SerializeField]    bool jumping;
    [SerializeField]    bool isGrounded;
	// Use this for initialization
	void Awake () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        jumpButtonPressed =  Input.GetButton(buttonName);
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
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        isGrounded = true;
        jumping = false;
        if(hexagon)
            hexagon.Wave( );
    }


    IEnumerator JumpRoutine()
    {
        rigidbody.velocity = Vector2.zero;

        while (jumpButtonPressed && timer < jumpTime)
        {
            //Calculate how far through the jump we are as a percentage
            //apply the full jump force on the first frame, then apply less force
            //each consecutive frame

           
            yield return null;
        }

        jumping = false;
    }
}
