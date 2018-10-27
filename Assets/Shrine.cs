using System.Collections;
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

    // Update is called once per frame
    void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && clickable && MouseOnObject)
        {
            if (!on && Physics.Raycast(ray))
            {
                this.GetComponent<AudioSource>().Play();
                on = true;
            }
            else if(on && Physics.Raycast(ray))
            {
                this.GetComponent<AudioSource>().Stop();
                on = false;
            }
        }

    }

}