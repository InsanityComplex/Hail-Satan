using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Radio : MonoBehaviour {

    public Dialogue[] Words;
    public bool UseAnimator;
    public bool UseAudio;

    bool clickable;
    bool on;
    bool setCompletition;
    bool firstEnable;

    Animator anim;
    AudioSource source;
    DialogueManager dMan;

    void Start(){
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        dMan = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();
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
                    if (UseAnimator)
                    {
                        anim.SetBool("on", on);
                    }

                    if (!setCompletition)
                    {
                        GameObject.Find("Player").SendMessage("UpdateCompletition");
                        setCompletition = true;
                    }

                    if (dMan.IsDialogueActive() && !on)
                    {
                        dMan.EndDialogue();
                    }
                    else if (on)
                    {
                        dMan.StartDialogue(new Queue<Dialogue>(Words));
                        firstEnable = false;
                    }

                    if (UseAudio && source.isPlaying && !on)
                    { 
                        source.Stop();
                    }
                    else if (UseAudio && !source.isPlaying && on)
                    {
                        source.Play();
                    }
                }
            }
        }
    }
}
