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
	[SerializeField]
	private GameObject qPopPanel; //! The questlist popup panel
	private Text textBox; //! The textbox of the panel
	private Button backButton; //! The back button
	private Button nextButton; //! The next button
	private List<string> tutorialDialogue; //! The tutorial dialogue list
	private List<Vector2> panelPos; //! The panel position

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		panel = transform.Find("Image").gameObject;
		textBox = panel.transform.Find("Text").GetComponent<Text>();
		backButton = panel.transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);
		backButton.gameObject.SetActive(false);
		nextButton = panel.transform.Find("NextButton").GetComponent<Button>();
		nextButton.onClick.AddListener(OnNextButtonClick);
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		tutorialProgress = 0;

		tutorialDialogue = new List<string>();
		tutorialDialogue.Add("Here, take this map of the city. It will help you navigate the streets with ease.");
		tutorialDialogue.Add("This is you! Your name, experience, and level are listed here. You can tap this spot to open the Profile screen.");
		tutorialDialogue.Add("Tapping this icon will open your Quest List. Tap here now, let’s see what’s available!");
		tutorialDialogue.Add("This screen lists all of the currently available Quests in the city. Once you complete one, it will be listed here under “Complete”.");
		tutorialDialogue.Add("Quests in green will take 15 minutes or less to complete. Yellow Quests are a bit longer, but you can still finish them in under 30 minutes.");
		tutorialDialogue.Add("Quests in red are longer and more complicated than the others. They can often take longer than 30 minutes to finish!");
		tutorialDialogue.Add("Tap here on the “Grim Beginnings” Quest, and we can get you started on exploring the city.");
		tutorialDialogue.Add("This is the description of the Quest, filled with useful information about what you’ll be doing.");
		tutorialDialogue.Add("You have to make a Quest active to make progress toward it. Tap here to accept the Quest.");
		tutorialDialogue.Add("Now “Grim Beginnings” is set as your Active Quest. You can drop your Active Quest if you want, but be careful!");
		tutorialDialogue.Add("If you drop your Active Quest you will have to start it over from the beginning!");
		tutorialDialogue.Add("Tap here to return to your Map.");
		tutorialDialogue.Add("First, we will need to take you to meet the Archbishop of Dubrovnik.");
		tutorialDialogue.Add("The city is not too fond of outsiders, so you will need the Archbishop’s blessing to move freely within the walls.");
		tutorialDialogue.Add("Come, meet me by the Pile Gate when you are ready to go, my friend. Here, I will mark it on your map.");
		tutorialDialogue.Add("I will show you the beauty of our fine city, and why we must fight to protect it!");
		tutorialDialogue.Add("Move your feet! What Lies Beneath is a game that uses your phone’s GPS to move through the game!");
		tutorialDialogue.Add("Eyes up! You will be exploring Dubrovnik in the present and in the past! Don’t miss out on the beauty of the Old Town!");

		textBox.text = tutorialDialogue[tutorialProgress];

		panelPos = new List<Vector2>();

		panelPos.Add(new Vector2(0, 350));
		panelPos.Add(new Vector2(0, -250));
		panelPos.Add(new Vector2(0, -500));
		panelPos.Add(new Vector2(0, 400));
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
		if(uiControl.DoTutOverlay)
		{
			gameObject.SetActive(true);
			panel.SetActive(true);
			GetComponent<Canvas>().sortingOrder = 15;
		}

		else
		{
			gameObject.SetActive(false);
			panel.SetActive(false);
		}
	}

	/*! \brief Called when the back button is clicked
	 */
	protected void OnBackButtonClick()
	{
		backPressed = true;
		tutorialProgress--;

		SetText();

		if(tutorialProgress <= 0)
		{
			backButton.gameObject.SetActive(false);
		}
	}

	/*! \brief Called when the next button is clicked
	 */
	private void OnNextButtonClick()
	{
		backPressed = false;
		tutorialProgress++;

		if(tutorialProgress >= tutorialDialogue.Count)
		{
			uiControl.TutorialActive = false;
			uiControl.DoTutOverlay = false;
			uiControl.UpdateTutorial();
			return;
		}

		SetText();

		if(tutorialProgress > 0)
		{
			backButton.gameObject.SetActive(true);
		}
	}

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
			//This case is used by states that set the next button to inactive
			case -1:
				nextButton.gameObject.SetActive(false);
				//panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
				break;

			case 0:
				uiControl.SetCanvas(UIState.MAP);
				goto default;

			case 1:
				panel.transform.localPosition = new Vector3(0, 350, 0);
				goto case 0;

			case 2:
				panel.transform.localPosition = new Vector3(0, -250, 0);
				nextButton.gameObject.SetActive(false);
				uiControl.SetCanvas(UIState.MAP);
				break;

			case 3:
			case 10:
				panel.transform.localPosition = new Vector3(0, -500, 0);
				transform.Find("../QuestCanvas").GetComponent<QListCanvas>().SetBackButton(false);
				goto default;

			case 5:
				transform.Find("../QuestCanvas").GetComponent<QListCanvas>().SetGrimBeginnings(false);
				goto default;

			case 6:
				transform.Find("../QuestCanvas").GetComponent<QListCanvas>().SetGrimBeginnings(true);
				qPopPanel.SetActive(false);
				goto case -1;

			case 8:
				transform.parent.GetComponent<QControl>().SetCurrentQuest(null);
				transform.Find("../QuestCanvas").GetComponent<QListCanvas>().ActivateQuestAccept();

				if(backPressed)
				{
					tutorialProgress = 6;
					SetText();
					break;
				}
				goto case -1;

			case 11:
				panel.transform.localPosition = new Vector3(0, 400, 0);
				if(backPressed)
					uiControl.SetCanvas(UIState.QLIST);
				transform.Find("../QuestCanvas").GetComponent<QListCanvas>().SetBackButton(true);
				goto case -1;

			case 12:
				panel.transform.localPosition = new Vector3();
				goto default;

			//This case is used by states that set the next button in active
			default:
				nextButton.gameObject.SetActive(true);
				//panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
				break;
		}
	}

	/*! \brief Used to progress dialogue from other places
	 */
	public void SpecialClick()
	{
		OnNextButtonClick();
	}

	/*! \brief Gets the tutorial progress
	 */
	public int TutorialProgress
	{
		get { return tutorialProgress; }
	}
}
