using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePopUp : MonoBehaviour {

    public Vector3 HiddenPos;
    public Vector3 ShownPos;
    public float MoveTime;
    public bool Show;

    RectTransform rect;
    private void Start()
    {
        rect = GetComponent<RectTransform>();   
    }

    void Update () {
        Vector3 cameraTarget = (Show) ? ShownPos : HiddenPos;
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, cameraTarget, MoveTime);
    }
}
