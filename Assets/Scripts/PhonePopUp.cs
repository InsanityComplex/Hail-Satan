using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePopUp : MonoBehaviour {

    public Vector3 HiddenPos;
    public Vector3 ShownPos;
    public float MoveTime;
    public bool Show;
    bool beginningDialogue;

    RectTransform rect;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update () {
        Vector3 cameraTarget = (Show) ? ShownPos : HiddenPos;
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, cameraTarget, MoveTime);
        if (Show && Vector3.Distance(rect.anchoredPosition, cameraTarget) < 10f && !beginningDialogue)
        {
            FindObjectOfType<DialogueUIManager>().BeginDialogue();
            beginningDialogue = true;
        }
        else if (!Show)
        {
            beginningDialogue = false; 
        }
    }
}
