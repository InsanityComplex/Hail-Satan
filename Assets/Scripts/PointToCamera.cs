using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PointToCamera : MonoBehaviour {
	void Update () {
        transform.forward = Camera.main.transform.forward;
	}
}
