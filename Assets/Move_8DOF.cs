using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Move_8DOF : MonoBehaviour {

    public float speed = 20.0f;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update() {

        //really bad code but whatever

        Vector3 loc = transform.position;

        loc.z += speed * Time.deltaTime;

    }
}
