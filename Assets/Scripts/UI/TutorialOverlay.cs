using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*! \class TutorialOverlay
 *	\brief Handles the tutorial overlay
 */
public class TutorialOverlay : AbstractCanvas
{
	private int tutorialProgress; //! The progress in the tutorial
	private bool backPressed; //! If the back button was pressed
	private GameObject panel; //! The panel for displaying tutorial dialogue
	private GameObject btn; //! The btn for displaying tutorial dialogue
	private GameObject lgBtn; //! The btn for displaying tutorial dialogue
	[SerializeField]
	private GameObject qPopPanel; //! The questlist popup panel
	private Text textBox; //! The textbox of the panel
	private List<string> tutorialDialogue; //! The tutorial dialogue list

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		panel = transform.Find("Panel").gameObject;

		btn = panel.transform.Find("Button").gameObject;
		btn.GetComponent<Button>().onClick.AddListener(OnBtnClick);
		textBox = btn.transform.Find("Text").GetComponent<Text>();

		lgBtn = textBox.gameObject.transform.Find("LetsGo").gameObject;
		lgBtn.GetComponent<Button>().onClick.AddListener(OnBtnClick);
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		tutorialProgress = 0;

		tutorialDialogue = new List<string>();
		tutorialDialogue.Add("Tap on the quest scroll at the bottom of the screen to see what tasks lie ahead.");
		tutorialDialogue.Add("As a newcomer to the city, you only have one available quest. As you finish tasks and explore the city, more will become available to you.");
		tutorialDialogue.Add("");
		tutorialDialogue.Add("I will meet you by the Pile Gate, one of the entrances to the Old Town. You can see how far you have to travel at the top of the screen.\n\nFollow the directional arrow until you see the location highlighted on your map.");
		tutorialDialogue.Add("Move Your Feet!\nWhat Lies Beneath is a game that requires you to move around the city\n\nEyes Up!\nYour phone will vibrate when you need to look at it. Don't miss out on the beauty of the city!");

		textBox.text = tutorialDialogue[tutorialProgress];
		Reset();
	}

	/*! \brief Updates the object
	 */
	protected override void Update()
	{

	}

	/*! \brief Updates the ui for the tutorial
	 */
	public override void UpdateTutorialUI()
	{
		if(uiControl.DoTutOverlay && tutorialProgress != 2)
		{
			gameObject.SetActive(true);
			panel.SetActive(true);
			GetComponent<Canvas>().sortingOrder = 2;
		}

		else
			gameObject.SetActive(false);
	}

	/*! \brief Called when the next button is clicked
	 */
	private void OnBtnClick()
	{
		tutorialProgress++;

		if(tutorialProgress >= tutorialDialogue.Count)
		{
			uiControl.TutorialActive = false;
			uiControl.DoTutOverlay = false;
			uiControl.UpdateTutorial();
			return;
		}

		SetText();
	}

	/*! \brief Sets the text of the tutorial dialogue
	 */
	private void SetText()
	{
		textBox.text = tutorialDialogue[tutorialProgress];
		CanvasSwitch();
	}

	/*! \brief changes the properties for the canvas
	 */
	private void CanvasSwitch()
	{
		switch(tutorialProgress)
		{
			case 0:
				panel.transform.localPosition = new Vector3(10, -100, 0);
				uiControl.SetCanvas(UIState.MAP);
				Reset();
				break;
			case 1:
				panel.transform.localPosition = new Vector3(10, -325, 0);
				break;
			case 2:
				panel.SetActive(false);
				break;
			case 3:
				btn.GetComponent<Button>().interactable = true;
				panel.transform.localPosition = new Vector3(-5, 100, 0);
				break;
			case 4:
				panel.transform.Find("Alek").gameObject.SetActive(false);
				btn.GetComponent<Button>().interactable = false;
				lgBtn.SetActive(true);
				panel.transform.localPosition = new Vector3(0, 200, 0);
				break;
		}
	}

	private void Reset()
	{
		panel.transform.Find("Alek").gameObject.SetActive(true);
		lgBtn.SetActive(false);
		btn.GetComponent<Button>().interactable = false;
	}

	/*! \brief Used to progress dialogue from other places
	 */
	public void SpecialClick()
	{
		OnBtnClick();
	}

	/*! \brief Gets the tutorial progress
	 */
	public int TutorialProgress
	{
		get { return tutorialProgress; }
	}
}
