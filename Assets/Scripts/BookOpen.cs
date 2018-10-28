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
            GetComponent<BoxCollider>().center = new Vector3(0.1f, 0.25f, 0f);
            GetComponent<BoxCollider>().size = new Vector3(0.75f, 0.5f, .1f);
            Open = true;    
        }
    }
}
