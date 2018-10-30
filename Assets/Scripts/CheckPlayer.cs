using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour {

    public Dialogue[] NotReady;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && other.gameObject.GetComponent<PlayerManager>().IsComplete())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else {
            Debug.Log(other);
            GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().StartDialogue(new Queue<Dialogue>(NotReady));
        }
    }
}
