using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float o2Duration;

    private Timer o2Timer;
	// Use this for initialization
	void Start () {
        o2Timer = new Timer("bob", o2Duration, false, KillPlayer);
	}
	
	// Update is called once per frame
	void Update () {
		if(o2Timer != null)
        {
            o2Timer.UpdateClock();
        }
	}

    private void KillPlayer()
    {
        Debug.Log("Player is dead");
    }
}
