using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Radio : MonoBehaviour {

    private bool clickable = false;
    private bool MouseOnObject = false;

    public bool on = false;

    Animator anim;
    // Use this for initialization
    void Start(){
        anim = GetComponentInChildren<Animator>();
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
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && clickable)
        {
            if (!on)
            {
                on = true;
            }

            else
            {
                on = false;
            }

            anim.SetBool("on", on);
        }

    }
}
