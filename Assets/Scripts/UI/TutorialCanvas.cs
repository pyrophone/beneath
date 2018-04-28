﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*! \class TutorialCanvas
 *	\brief Handles the tutorial canvas interaction
 */
public class TutorialCanvas : AbstractCanvas
{
	private int dialCount; //! The count for the tutorial dialogue
	private Text dialogueBox; //! The textbox for the dialogue
	private Button nextButton; //! The button to progess dialogue
	private List<string> dialogue; //! This is temporary. Keeps dialogue for tutorial

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();
        
        // dialogueBox = transform.Find("DialogueBox").GetComponentInChildren<Text>();
        dialogueBox = transform.Find("DialogueBox").GetComponentInChildren<Text>();
		nextButton = transform.Find("Button").GetComponent<Button>();
		nextButton.onClick.AddListener(OnNextButtonClick);
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		dialCount = 0;

		dialogue = new List<string>();
		dialogue.Add("Hello and welcome to the city of Dubrovnik!");
		dialogue.Add("My name is Aleksander. I am what is known as a ‘krsnik’.");
		dialogue.Add("The krsnik are local shamans, allied with the church here.");
		dialogue.Add("We work with the church to seek and destroy monsters, wherever they hide.");
		dialogue.Add("You are the new agent, sent from the Vatican, yes? What is your name?");
		dialogue.Add("");
		dialogue.Add("Ah, I see! A lovely name. Well, -----, let me explain what is going on.");
		dialogue.Add("Dubrovnik, you see, has been infested with an especially vile monster...");
		dialogue.Add("The vampire! The vile bloodsucking fiends have taken up residence in this beautiful city.");
		dialogue.Add("They have been stealing innocent lives nearly every night.");
		dialogue.Add("Normally, the krsnik and the local agents of the church would be enough to fend them off.");
		dialogue.Add("But this time… something is different. We think that the creatures are planning something.");
		dialogue.Add("We have called for your aid. We need someone with your skills to help us defeat the vampire menace.");
	}

	/*! \brief Updates the object
	 */
	protected override void Update()
	{
		dialogueBox.text = dialogue[dialCount];
	}

	/*! \brief Called when the next button is pressed
	 */
	private void OnNextButtonClick()
	{
		dialCount++;

		if(dialCount == 5)
			transform.parent.Find("NameEnterCanvas").gameObject.SetActive(true);

		else if(dialCount == 6)
			dialogue[dialCount] = dialogue[dialCount].Replace("-----", uiControl.PName);

		else if(dialCount >= dialogue.Count)
		{
			uiControl.DoTutOverlay = true;
			uiControl.SetCanvas(UIState.MAP);
		}
	}

	public void SpecialClick()
	{
		if(uiControl.TutorialActive)
			OnNextButtonClick();
	}
}
