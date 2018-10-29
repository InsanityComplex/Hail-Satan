using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour {

    public float Speed;
    public Transform Target;
    public Vector3 Offset;

	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, Target.position + Offset, Speed);
	}
}
