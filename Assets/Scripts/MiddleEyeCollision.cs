using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleEyeCollision : MonoBehaviour {

    public transformation tr;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hit Something");
        tr.lookingAtEyes = true;
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("Hit Something");
        tr.lookingAtEyes = false;
    }
}
