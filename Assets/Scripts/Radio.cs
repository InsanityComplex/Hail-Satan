using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Radio : MonoBehaviour {

    private bool clickable = false;
    public bool MouseOnObject = false;

    public bool on = false;
    public Dialogue[] Words;
    public bool UseAnimator;

    bool firstEnable;
    Animator anim;
    AudioSource source;
    // Use this for initialization
    void Start(){
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        
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
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mouseRay, out info, Mathf.Infinity, 1, QueryTriggerInteraction.Collide))
            {
                if (info.collider.gameObject.name == gameObject.name && clickable)
                {
                    on = !on; 
                }
            }
        }
        if (on && !source.isPlaying)
        {
            if (!firstEnable)
            {
                firstEnable = true;
            }
            source.Play();
        }
        else if (source.isPlaying && !on)
        {
            source.Stop();
            GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().EndDialogue();
        }

        if (UseAnimator)
        {
            anim.SetBool("on", on);
        }
        
        if (firstEnable && Words.Length > 0)
        {
            GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().StartDialogue(new Queue<Dialogue>(Words));
            firstEnable = false;
        }
    }
}
