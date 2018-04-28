using System.Collections;
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
        dialogueBox = transform.Find("Character").Find("DialogueBox").GetComponentInChildren<Text>();
		nextButton = transform.Find("Button").GetComponent<Button>();
		nextButton.onClick.AddListener(OnNextButtonClick);
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		dialCount = 0;

		dialogue = new List<string>();
		dialogue.Add("Hello there! Welcome to the city of Dubrovnik. You must be the explorer I've heard so much about. We've been expecting you. Allow me to introduce myself. I am Aleksander, and I will be your guide.");
	}

	/*! \brief Updates the object
	 */
	protected override void Update()
	{
		if(dialCount < dialogue.Count)
			dialogueBox.text = dialogue[dialCount];
	}

	/*! \brief Called when the next button is pressed
	 */
	private void OnNextButtonClick()
	{
		dialCount++;

		if(dialCount == 1)
			transform.parent.Find("NameEnterCanvas").gameObject.SetActive(true);

		//else if(dialCount == 6)
		//	dialogue[dialCount] = dialogue[dialCount].Replace("-----", uiControl.PName);

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
