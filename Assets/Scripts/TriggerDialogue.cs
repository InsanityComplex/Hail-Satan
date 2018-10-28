using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour {

    public Dialogue[] Words;

    Queue<Dialogue> diaQueue;
  
    public void Start()
    {
        diaQueue = new Queue<Dialogue>();
        for (int i = 0; i < Words.Length; i++)
        {
            diaQueue.Enqueue(Words[i]);
        }
        

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().StartDialogue(diaQueue);
            Debug.Log("Allowing interaction");
        }
    }

    public void DisableThis()
    {

    }
}
