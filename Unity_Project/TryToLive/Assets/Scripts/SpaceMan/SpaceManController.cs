using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceManController : MonoBehaviour {

    Rigidbody2D rb2D;
    Vector2 startDrift = new Vector2(5,4);
    public GameObject rightArm;
    public GameObject leftArm;

	// Use this for initialization
	void Awake () {
		rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.AddForce(startDrift);
    }
	
	// Update is called once per frame
	void Update () {
        RotateInSpace();



    }

    private float horizontal;

    private void RotateInSpace()
    {
        horizontal = Input.GetAxis("Horizontal");

        rb2D.angularVelocity = horizontal * -100;


    }
}
