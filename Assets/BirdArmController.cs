using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdArmController : MonoBehaviour {
	
	void Start () {
        Cursor.lockState = CursorLockMode.None;
	}
	
	
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
	}
}
