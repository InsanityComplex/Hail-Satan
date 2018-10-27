using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour {

    private bool clickable = false;

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

        if (Input.GetMouseButtonDown(0) && clickable)
        {
            Debug.Log("Clicked");
            this.GetComponent<AudioSource>().Play();
        }

    }


}
