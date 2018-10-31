using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour {

    public string NodeName;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(NodeName);
            Debug.Log("Allowing interaction");
        }
    }

    public void DisableThis()
    {

    }

}
