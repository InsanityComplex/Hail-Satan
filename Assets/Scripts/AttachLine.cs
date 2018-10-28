using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachLine : MonoBehaviour {

    public Transform AttachPoint;

    LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, AttachPoint.position);
	}
}
