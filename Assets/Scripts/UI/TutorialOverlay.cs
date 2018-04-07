using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class TutorialOverlay : AbstractCanvas
{
	private int tutorialProgress;
	private bool backPressed;
	private GameObject panel;
	private Text textBox;
	private Button backButton;
	private Button nextButton;
	private List<string> tutorialDialogue;
	private List<Vector2> panelPos;

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
			GetComponent<Canvas>().sortingOrder = 10;
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

		textBox.text = tutorialDialogue[tutorialProgress];
		CanvasSwitch();

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

		textBox.text = tutorialDialogue[tutorialProgress];
		CanvasSwitch();

		if(tutorialProgress > 0)
		{
			backButton.gameObject.SetActive(true);
		}
	}

	private void CanvasSwitch()
	{


		//else if(tutorialProgress == 2)

		//else if(tutorialProgress == 11)



		switch(tutorialProgress)
		{
			//This case is used by states that set the next button to inactive
			case -1:
				nextButton.gameObject.SetActive(false);
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
				goto default;

			case 6:
				transform.Find("../QuestCanvas").GetComponent<QListCanvas>().ActivateGrimBeginnings();
				goto case -1;

			case 8:
				transform.parent.GetComponent<QControl>().SetCurrentQuest(null);
				transform.Find("../QuestCanvas").GetComponent<QListCanvas>().ActivateQuestAccept();

				if(backPressed)
				{
					tutorialProgress = 7;
					OnBackButtonClick();
					break;
				}
				goto case -1;

			case 11:
				panel.transform.localPosition = new Vector3(0, 400, 0);
				if(backPressed)
					uiControl.SetCanvas(UIState.QLIST);
				transform.Find("../QuestCanvas").GetComponent<QListCanvas>().ActivateBackButton();
				goto case -1;

			case 12:
				panel.transform.localPosition = new Vector3();

			//This case is used by states that set the next button in active
			default:
				nextButton.gameObject.SetActive(true);
				break;
		}

		//if(tutorialProgress <= 2 || tutorialProgress == tutorialDialogue.Count)
		//{
		//	switch(tutorialProgress)
		//	{
		//		case 2:
		//			panel.transform.localPosition = new Vector3(0, -250, 0);
		//			nextButton.gameObject.SetActive(false);
		//			break;
		//		case 1:
		//			panel.transform.localPosition = new Vector3(0, 350, 0);
		//		default:
		//			nextButton.gameObject.SetActive(true);
		//			break;
		//	}

		//	uiControl.SetCanvas(UIState.MAP);
		//}

		//else if(tutorialProgress == 3 || tutorialProgress == 10)
		//	panel.transform.localPosition = new Vector3(0, -500, 0);

		//else if(tutorialProgress == 6 || tutorialProgress == 8 || tutorialProgress == 11)
		//{
		//	nextButton.gameObject.SetActive(false);

		//	switch(tutorialProgress)
		//	{
		//		case 6:
		//			transform.Find("../QuestCanvas").GetComponent<QListCanvas>().ActivateGrimBeginnings();
		//			break;
		//		case 8:
		//			transform.parent.GetComponent<QControl>().SetCurrentQuest(null);
		//			transform.Find("../QuestCanvas").GetComponent<QListCanvas>().ActivateQuestAccept();

		//			if(backPressed)
		//				tutorialProgress = 6;
		//			break;
		//		case 11:
		//			panel.transform.localPosition = new Vector3(0, 400, 0);
		//			if(backPressed)
		//				uiControl.SetCanvas(UIState.QLIST);
		//			transform.Find("../QuestCanvas").GetComponent<QListCanvas>().ActivateBackButton();
		//			break;
		//		default:
		//			break;
		//	}
		//}

		//else if(tutorialProgress == 12)
		//	panel.transform.localPosition = new Vector3();

		//else
		//	nextButton.gameObject.SetActive(true);
	}

	public void SpecialClick()
	{
		OnNextButtonClick();
	}

	public int TutorialProgress
	{
		get { return tutorialProgress; }
	}
}
