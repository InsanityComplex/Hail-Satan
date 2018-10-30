using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFPS : MonoBehaviour {

	public int Target;
	void Start () {
		Application.targetFrameRate = Target;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
