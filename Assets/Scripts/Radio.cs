using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Radio : MonoBehaviour {

    public string NodeName;
    public bool UseAnimator;
    public bool UseAudio;

    int currentConversation;
    AudioClip currentClip;
    bool clickable;
    bool on;
    bool setCompletition;
    bool firstEnable;

    Animator anim;
    AudioSource source;
    Yarn.Unity.DialogueRunner runner;

    void Start(){
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        runner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    void Interact()
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

        else if (on || !(UseAnimator || UseAudio))
        {
            runner.StartDialogue(NodeName);
            firstEnable = false;
        }

        if (UseAudio && source.isPlaying && !on)
        {
            source.Stop();
        }
        else if (UseAudio && !source.isPlaying && on)
        {
            if (source.clip != currentClip)
            {
                source.clip = currentClip;
            }
            source.Play();
        }
    }
}
