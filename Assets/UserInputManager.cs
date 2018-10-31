using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    public DialogueUIManager DMan;
    public PlayerMovement Player;
    public LayerMask Mask;
    public Vector3 Offset;

    private void Update()
    {
        Vector3 interactPos;
        bool isInteracting;
        if (Input.touchSupported && Input.touches.Length > 0)
        {
            interactPos = Input.GetTouch(0).position;
            isInteracting = Input.GetTouch(0).phase == TouchPhase.Began;
        }
        else
        {
            interactPos = Input.mousePosition;
            isInteracting = Input.GetMouseButtonDown(0);
        }

        if (isInteracting && !DMan.IsDialogueActive())
        {
            Debug.Log("in statement");
            Ray interactRay = Camera.main.ScreenPointToRay(interactPos);
            RaycastHit info;
            if (Physics.Raycast(interactRay, out info, Mathf.Infinity, Mask, QueryTriggerInteraction.Ignore))
            {
                if (info.transform.gameObject.layer == 15) {
                
                    Player.Move(info.transform.position - Offset);
                    info.transform.gameObject.SendMessage("Interact");
                }
                else
                {
                    Player.Move(info.point);
                }
            } 
        }
    }
}
