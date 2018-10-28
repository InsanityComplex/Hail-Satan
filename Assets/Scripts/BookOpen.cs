using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookOpen : MonoBehaviour {

    public Sprite OpenState;
    public Sprite BloodiedState;
    public float OpenForce;
    public bool Open;

    bool sent;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > OpenForce)
        {
            GetComponent<SpriteRenderer>().sprite = OpenState;
            GetComponent<BoxCollider>().center = new Vector3(-.125f, 0.25f, 0.0125f);
            GetComponent<BoxCollider>().size = new Vector3(0.45f, 0.5f, .075f);
            Open = true;    
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.parent.name == "bird_wing_r" && !sent)
        {
            if (other.gameObject.GetComponentInParent<IBleed>().Bleeding)
            {
                GetComponent<SpriteRenderer>().sprite = BloodiedState;
                GameObject.Find("Main Camera").SendMessage("RevealSatan");
                sent = true;
            }
        }
    }
}
