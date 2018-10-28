using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookOpen : MonoBehaviour {

    public Sprite OpenState;
    public float OpenForce;
    public bool Open;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > OpenForce)
        {
            GetComponent<SpriteRenderer>().sprite = OpenState;
            GetComponent<BoxCollider>().center = new Vector3(0.3f, 0.25f, 0f);
            GetComponent<BoxCollider>().size = new Vector3(0.45f, 0.5f, .075f);
            GetComponent<BoxCollider>().isTrigger = true;
            Open = true;    
        }
    }
}
