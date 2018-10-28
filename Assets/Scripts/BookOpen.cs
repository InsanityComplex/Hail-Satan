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
            Open = true;    
        }
    }
}
