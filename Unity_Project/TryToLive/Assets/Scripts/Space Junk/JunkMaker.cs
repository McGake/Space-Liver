using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkMaker : MonoBehaviour {

    public GameObject junk;
    public int howMuchJunk;



    private void Start() {
        for (int i = 0; i < howMuchJunk; i++) {
            Instantiate(junk, transform.position, transform.rotation);
        }
    }


}
