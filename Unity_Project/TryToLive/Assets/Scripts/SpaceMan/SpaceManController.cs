using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceManController : MonoBehaviour {

    Rigidbody2D rb2D;
    Vector2 startDrift = new Vector2(2.5f,2);
    public GameObject rightArmJoint;
    public GameObject leftArmJoint;

    public GameObject bloopTester;

	// Use this for initialization
	void Awake () {
		rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.AddForce(startDrift);
    }
	
	// Update is called once per frame
	void Update () {
        RotateInSpace();
        MoveArms();

    }

    private float horizontal;


    private void RotateInSpace()
    {
        horizontal = Input.GetAxis("Horizontal");

        rb2D.angularVelocity = horizontal * -100;

    }

    private float horizontal2;
    private float vertical2;
    public float armRotSpeed = 2;

    private Vector2 direction;
    private float angle;

    private Vector2 tempPosBloop = new Vector2();


    Quaternion targetRotation;
   

    private void MoveArms()
    {
        horizontal2 = Input.GetAxis("Horizontal2");
        vertical2 = Input.GetAxis("Vertical2");
        tempPosBloop.y = transform.position.y + -vertical2;
        tempPosBloop.x = transform.position.x + horizontal2;
        bloopTester.transform.position = tempPosBloop;

        direction.y = horizontal2;
        direction.x = -vertical2;
        if ((Mathf.Abs(horizontal2) + Mathf.Abs(vertical2)) > .1f)
        {
            direction.Normalize();

            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            leftArmJoint.transform.rotation = Quaternion.Slerp(leftArmJoint.transform.rotation, targetRotation, armRotSpeed * Time.deltaTime);

            rightArmJoint.transform.rotation = Quaternion.Slerp(rightArmJoint.transform.rotation, targetRotation, armRotSpeed * Time.deltaTime);
        }
    }
}
