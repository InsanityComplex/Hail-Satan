using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour {

	public GameObject[] ObjectsToDisable;
	public GroupDisable[] ScriptsToDisable;
	public Transform DialoguePanel;
	public TextMeshProUGUI Speaker;
	public TextMeshProUGUI Dialogue;
	public TextMeshProUGUI[] ChoiceTexts;
	public Transform ChoicePanel;
	public float CrawlTime;
    public PhonePopUp Phone;

	Coroutine textCrawl;
	Queue<Dialogue> dialogueQueue;
	Dialogue currentDialogue;
	bool currentFullyDisplayed;


	// Use this for initialization
	void Start () {
		EndDialogue();
	}

	public void StartDialogue(Queue<Dialogue> queue) 
	{
        if (dialogueQueue != null)
        {
            return;
        }

        dialogueQueue = queue;

        foreach (GameObject g in ObjectsToDisable)
        {
            g.SetActive(false);
        }
        foreach (GroupDisable g in ScriptsToDisable)
        {
            g.enabled = false;
        }

        Phone.Show = true;
	}

    public void BeginDialogue()
    {
        DialoguePanel.gameObject.SetActive(true);
        NextDialogue();
    }

	public void NextDialogue() 
	{
		if (dialogueQueue != null && dialogueQueue.Count > 0) 
		{
			currentDialogue = dialogueQueue.Dequeue();
			ChoicePanel.gameObject.SetActive(false);
			Speaker.text = currentDialogue.Speaker;
			textCrawl = StartCoroutine(TextCrawl(currentDialogue));
		}
		else 
		{
			EndDialogue();
			currentDialogue = null;
		}
	}

	void EnableChoices() 
	{
		ChoicePanel.gameObject.SetActive(true);
		for (int i = 0; i < currentDialogue.Choices.Length; i++) 
		{
			ChoiceTexts[i].transform.parent.gameObject.SetActive(true);
			ChoiceTexts[i].text = currentDialogue.Choices[i];

		}
		for (int i = currentDialogue.Choices.Length; i < ChoiceTexts.Length; i++) 
		{
			ChoiceTexts[i].transform.parent.gameObject.SetActive(false);
		}
	}

	public void EndDialogue() 
	{
        foreach (GameObject g in ObjectsToDisable)
        {
            g.SetActive(true);
        }
        foreach (GroupDisable g in ScriptsToDisable)
        {
            g.enabled = true;
        }
        StopAllCoroutines();

        DialoguePanel.gameObject.SetActive(false);
		ChoicePanel.gameObject.SetActive(false);
        Phone.Show = false;
        dialogueQueue = null;
    }

	IEnumerator TextCrawl(Dialogue dia) 
	{
		string str = "";
		currentFullyDisplayed = false;
		for (int i = 0; i < dia.Sentence.Length; i++) 
		{
			str += dia.Sentence[i];
			Dialogue.text = str;
			yield return new WaitForSecondsRealtime(CrawlTime);
		}
		if (dia.Action != null) 
		{
			dia.Action();
		}
		if (dia.Choices.Length > 0) 
		{
			EnableChoices();
		}
		currentFullyDisplayed = true;
	}

    public bool IsDialogueActive()
    {
        return dialogueQueue != null;
    }

	void Update() 
	{
		if (currentDialogue != null)
		{
			if (Input.GetButtonDown("Submit")) 
			{
				if (currentDialogue.Choices.Length > 0) 
				{
					//do nothing
				}
				else if (currentFullyDisplayed)
				{
					currentFullyDisplayed = false;
					NextDialogue();
				}
			}
		}
	}

}

[Serializable]
public class Dialogue 
{
    public string Sentence;
    public string Speaker;
	public string[] Choices;
    public delegate void EndDialogueAction();
	public EndDialogueAction Action;

    public Dialogue(string speak, string sent) : this(speak, sent, new string[0], null) { }
    public Dialogue(string speak, string sent, string[] choices) : this(speak, sent, choices, null) { }
    public Dialogue(string speak, string sent, EndDialogueAction act) : this(speak, sent, new string[0], act) { }
	public Dialogue(string speak, string sent, string[] choices, EndDialogueAction act) 
	{
		Speaker = speak;
		Sentence = sent;
		Choices = choices;
		Action = act;
	}
}

[Serializable]
public class DialogueContainer
{
    public string Name;
    public Dialogue[] Conversations;
}
