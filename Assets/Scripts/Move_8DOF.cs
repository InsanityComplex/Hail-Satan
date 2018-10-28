using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_8DOF : GroupDisable {

    public float speed = 20.0f;

    CharacterController controller;
    Animator anim;

	void Start () {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
	}

    void Update() {
        Vector3 heading = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed;
        anim.SetInteger("Horizontal", (int)heading.x);
        anim.SetInteger("Vertical", (int)heading.z);
        heading += Physics.gravity;
        controller.Move(heading * Time.deltaTime);

    }
    private void OnDisable()
    {
        anim.SetInteger("Horizontal", 0);
        anim.SetInteger("Vertical", 0);
    }
}
