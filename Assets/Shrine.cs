﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shrine : MonoBehaviour {

    private bool clickable = false;
    private bool MouseOnObject = false;

    private bool on = false;


    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            clickable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            clickable = false;
        }
    }
 
    void OnMouseEnter()
    {
        MouseOnObject = true;
        Debug.Log("Mouse On");
    }

    void OnMouseExit()
    {
        MouseOnObject = false;
        Debug.Log("Mouse Off");
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0) && clickable)
        {
            if (!on)
            {
                this.GetComponent<AudioSource>().Play();
                on = true;
            }
            else
            {
                this.GetComponent<AudioSource>().Stop();
                on = false;
            }
        }

    }

}
