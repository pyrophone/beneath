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

		dialogueBox = transform.Find("TextPanel").Find("Dialogue").GetComponent<Text>();
		nextButton = transform.Find("Button").GetComponent<Button>();
		nextButton.onClick.AddListener(OnNextButtonClick);
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		dialCount = 0;

		dialogue = new List<string>();
		dialogue.Add("Hello and welcome to the city of Dubrovnik!\n\nMy name is Aleksander.\n\nWe've been expecting you.");
		dialogue.Add("");
		dialogue.Add("Welcome, -----.\n\nEach quest you complete will increase your knowledge of this ancient city, and help you unlock the mysteries of What Lies Beneath.\n\nShall we begin?");
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

		if(dialCount == 1)
			transform.parent.Find("NameEnterCanvas").gameObject.SetActive(true);

		else if(dialCount == 2)
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
