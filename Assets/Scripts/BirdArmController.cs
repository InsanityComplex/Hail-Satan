using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdArmController : MonoBehaviour {

    public float DragStrength;
    Rigidbody hand;

	void Start () {
        Cursor.lockState = CursorLockMode.None;
        
	}
	
	
	void FixedUpdate () {
        Vector3 mousePos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 2));
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit info;
            if (Physics.Raycast(mouseRay, out info, Mathf.Infinity, Physics.AllLayers, QueryTriggerInteraction.UseGlobal) && info.rigidbody.gameObject.name == "bird_wing")
            {
                hand = info.rigidbody;
                hand.gameObject.layer = 14;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (hand)
            {
                hand.gameObject.layer = 0;
            }
            hand = null;
        }

        if (hand)
        {
            if (Vector3.Distance(transform.position, hand.transform.position) < 0.25f)
            {
                hand.AddForce(Vector3.zero, ForceMode.VelocityChange);
            }
            hand.AddForce((transform.position - hand.transform.position) * DragStrength * Vector3.Distance(transform.position, hand.transform.position));
        }
    }
}
