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

        Vector3 heading = new Vector3();

        if (Input.GetKey("w"))
        {
            heading += Vector3.forward;
        }
        if (Input.GetKey("s"))
        {
            heading += Vector3.back;
        }

        if(Input.GetKey("d"))
        {
            heading += Vector3.right;
        }
        if(Input.GetKey("a"))
        {
            heading += Vector3.left;
        }

        transform.position += heading.normalized * speed * Time.deltaTime;

    }
}
