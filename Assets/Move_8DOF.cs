using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Move_8DOF : MonoBehaviour {

    public float speed = 20.0f;

    CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update() {

        //really bad code but whatever

        Vector3 heading = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(heading.normalized * speed * Time.deltaTime);

    }
}
