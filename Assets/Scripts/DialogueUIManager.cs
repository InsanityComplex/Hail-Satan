using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;


public class DialogueUIManager : Yarn.Unity.DialogueUIBehaviour
{
    public GameObject[] ObjectsToDisable;
    public GroupDisable[] ScriptsToDisable;
    public Transform DialoguePanel;
    public TextMeshProUGUI Speaker;
    public TextMeshProUGUI Dialogue;
    public TextMeshProUGUI[] ChoiceTexts;
    public Transform ChoicePanel;
    public float CrawlTime;
    public PhonePopUp Phone;

    Yarn.OptionChooser SetSelectedOption;
    Regex speakerTester = new Regex(@"^.+:\s");
    bool phoneAppeared;
    bool dialogueActive;

    private void Awake()
    {
        foreach (GameObject go in ObjectsToDisable)
        {
            go.SetActive(true);
        }

        foreach (GroupDisable gd in ScriptsToDisable)
        {
            gd.Active = true;
        }

        DialoguePanel.gameObject.SetActive(false);
        ChoicePanel.gameObject.SetActive(false);

        phoneAppeared = false;
        Phone.Show = false;
    }

    public void BeginDialogue()
    {
        phoneAppeared = true;
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    } 

    public override IEnumerator RunLine(Yarn.Line line)
    {
        DialoguePanel.gameObject.SetActive(true);
        string speaker = speakerTester.Match(line.text).Value;
        string dialogue = line.text.Remove(0, speaker.Length);
        Speaker.text = speaker;

        string str = "";
        for (int i = 0; i < dialogue.Length; i++)
        {
            str += dialogue[i];
            Dialogue.text = str;
            yield return new WaitForSecondsRealtime(CrawlTime);
        }

        // Wait for any user input
        while (Input.anyKeyDown == false)
        {
            yield return null;
        }
    }

    public override IEnumerator RunOptions(Yarn.Options optionsCollection,
                                                Yarn.OptionChooser optionChooser)
    {
        ChoicePanel.gameObject.SetActive(true);
        int i = 0;
        foreach (var optionString in optionsCollection.options)
        {
            ChoiceTexts[i].transform.parent.gameObject.SetActive(true);
            ChoiceTexts[i].text = optionString;
            i++;
        }

        // Record that we're using it
        SetSelectedOption = optionChooser;

        // Wait until the chooser has been used and then removed (see SetOption below)
        while (SetSelectedOption != null)
        {
            yield return null;
        }

        // Hide all the buttons
        foreach (var button in ChoiceTexts)
        {
            button.transform.parent.gameObject.SetActive(false);
        }
    }

    public override IEnumerator RunCommand(Yarn.Command command)
    {
        Debug.Log(command);
        yield return null;
    }

    public void SetOption(int selectedOption)
    {

        // Call the delegate to tell the dialogue system that we've
        // selected an option.
        SetSelectedOption(selectedOption);

        // Now remove the delegate so that the loop in RunOptions will exit
        SetSelectedOption = null;
    }

    /// Called when the dialogue system has started running.
    public override IEnumerator DialogueStarted()
    {
        dialogueActive = true;
        Phone.Show = true;
        foreach (GameObject go in ObjectsToDisable)
        {
            go.SetActive(false);
        }

        foreach (GroupDisable gd in ScriptsToDisable)
        {
            gd.Active = false;
        }
        
        while (!phoneAppeared)
        {
            yield return null;
        }

        yield break;
    }

    public override IEnumerator DialogueComplete()
    {
        dialogueActive = false;

        foreach (GameObject go in ObjectsToDisable)
        {
            go.SetActive(true);
        }

        foreach (GroupDisable gd in ScriptsToDisable)
        {
            gd.Active = true;
        }

        DialoguePanel.gameObject.SetActive(false);
        ChoicePanel.gameObject.SetActive(false);

        phoneAppeared = false;
        Phone.Show = false;

        yield break;
    }
}


/*public class DialogueManager : MonoBehaviour {



	
    public float SkipTime;
    public PhonePopUp Phone;

    float timer;
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
            g.Active = false;
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
            currentFullyDisplayed = false;
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

    public void SkipToNext()
    {
        if (timer > SkipTime && !currentFullyDisplayed)
        {
            StopCoroutine(textCrawl);
            if (currentDialogue.Action != null)
            {
                currentDialogue.Action();
            }
            EnableChoices();
            Dialogue.text = currentDialogue.Sentence;
            currentFullyDisplayed = true;
            timer = 0f;
        }
        else if (currentFullyDisplayed)
        {
            NextDialogue();
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
            g.Active = true;
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
        timer += Time.deltaTime;
		if (currentFullyDisplayed)
        {
            timer = 0f;
        }
	}

}
*/
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
    public AudioClip Audio;
}

