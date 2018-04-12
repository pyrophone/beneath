using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*! \class QListCanvas
 *	\brief Handles the Quest List canvas interaction
 */
public class QListCanvas : AbstractCanvas
{
	private QControl qControl; //! Reference to the Quest controller
	private Button backButton; //! Reference to the back button
	private Button activeQuestButton; //! Reference to the active quest button
	private GameObject scrollContent; //! Scroll content list
	[SerializeField]
	private GameObject btnObj; //! Used to instantiate buttons for the scroll view.
	private GameObject popUpPanel; //! The panel that pops up for confirmations

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		scrollContent = transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
		popUpPanel = transform.Find("PopUpPanel").gameObject;
		popUpPanel.SetActive(false);
		qControl = transform.parent.GetComponent<QControl>();

		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);

		activeQuestButton = transform.Find("ActiveQuestButton").GetComponent<Button>();
		activeQuestButton.onClick.AddListener(OnActiveButtonClick);
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{

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
		if(uiControl.TutorialActive)
		{
			backButton.interactable = false;
		}

		else
		{
			backButton.interactable = true;
		}
	}

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
		if(uiControl.TutorialActive)
			transform.Find("../TutorialOverlay").GetComponent<TutorialOverlay>().SpecialClick();

		uiControl.SetCanvas(UIState.MAP);
	}

	/*! \brief Called when the active quest button is clicked
	 */
	private void OnActiveButtonClick()
	{
		if(qControl.CurQuest != null)
		{
			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "Are you sure you want to cancel your current quest?";
			popUpPanel.SetActive(true);

			popUpPanel.transform.Find("YesButton").GetComponent<Button>().interactable = true;
			popUpPanel.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(() => { qControl.SetCurrentQuest(null); popUpPanel.SetActive(false); });

			popUpPanel.transform.Find("YesButton").Find("Text").GetComponent<Text>().text = "Yes";
			popUpPanel.transform.Find("NoButton").Find("Text").GetComponent<Text>().text = "No";

			uiControl.Dial.ResetDialogue();
		}
	}

	/*! \brief Getter / Setter for the active quest button
	 */
	public void SetActiveQuestText(string text)
	{
		transform.Find("ActiveQuestButton").Find("Text").GetComponent<Text>().text = text;
	}

	/*! \brief Refreshes the quest list
	 *
	 * \param (Dictionary<Quest, bool>) quests - Dictionary of the available quests
	 * \param (Quest) current - The current quest
	 */
	public void RefreshQuestList(Dictionary<Quest, bool> quests, Quest current)
	{
		Vector3 offset = new Vector3(0.0f, 75.0f, 0.0f);

		for(int i = scrollContent.transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(scrollContent.transform.GetChild(i).gameObject);
		}

		foreach(var quest in quests)
		{
			if(quest.Value == false && quest.Key != current)
			{
				GameObject btn = Instantiate(btnObj);
				btn.transform.SetParent(scrollContent.transform, false);
				btn.transform.Find("Text").GetComponent<Text>().text = quest.Key.name;
				btn.transform.localPosition += offset;
				//Funky anonymous function stuff to pass a param to a method
				btn.GetComponent<Button>().onClick.AddListener(() => { QuestButtonClick(quest.Key); } );

				offset -= new Vector3(0, 150.0f, 0.0f);
			}
		}

		for(int i = 0; i < scrollContent.transform.childCount; i++)
		{
			Transform child = scrollContent.transform.GetChild(i);
			if(uiControl.TutorialActive)
				child.GetComponent<Button>().interactable = false;
		}
	}

	/*! \brief Called when a quest button is clicked
	 *
	 *
	 * \param (Quest) q - The quest to try to switch
	 */
	public void QuestButtonClick(Quest q)
	{
		Quest prereq = qControl.GetQuest(q.prereqQuestID);
		popUpPanel.transform.Find("YesButton").Find("Text").GetComponent<Text>().text = "Accept";
		popUpPanel.transform.Find("NoButton").Find("Text").GetComponent<Text>().text = "Cancel";

		if(uiControl.TutorialActive)
			transform.Find("../TutorialOverlay").GetComponent<TutorialOverlay>().SpecialClick();

		popUpPanel.transform.Find("NoButton").GetComponent<Button>().interactable = true;

		if(prereq == null || prereq.completed == true)
		{
			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "Are you sure you want to accept " + q.name + "? This will cancel your current quest";
			popUpPanel.SetActive(true);

			popUpPanel.transform.Find("YesButton").GetComponent<Button>().interactable = true;

			//More funky anonymous function stuff
			if(uiControl.TutorialActive) {
				Button btn = popUpPanel.transform.Find("YesButton").GetComponent<Button>();
				btn.onClick.RemoveAllListeners();
				btn.onClick.AddListener(() => {
					qControl.SetCurrentQuest(q); popUpPanel.SetActive(false);
					transform.Find("../TutorialOverlay").GetComponent<TutorialOverlay>().SpecialClick();
				});
				btn.interactable = false;
				popUpPanel.transform.Find("NoButton").GetComponent<Button>().interactable = false;
			}

			else
				popUpPanel.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(() => { qControl.SetCurrentQuest(q); popUpPanel.SetActive(false); });

		}

		else
		{
			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "You must complete " + prereq.name + " before starting this quest.";
			popUpPanel.SetActive(true);

			popUpPanel.transform.Find("YesButton").GetComponent<Button>().interactable = false;
		}
	}

	/*! \brief Activates grim beginnings for the tutorial
	 *
	 * \param (bool) isSet if the button should be set
	 */
	public void SetGrimBeginnings(bool isSet)
	{
		if(uiControl.TutorialActive)
			scrollContent.transform.GetChild(0).GetComponent<Button>().interactable = isSet;
	}

	/*! \brief Activates the quest accept button for the tutorial
	 */
	public void ActivateQuestAccept()
	{
		if(uiControl.TutorialActive)
			popUpPanel.transform.Find("YesButton").GetComponent<Button>().interactable = true;
	}

	/*! \brief Activates the back button for the tutorial
	 */
	public void ActivateBackButton()
	{
		if(uiControl.TutorialActive)
			backButton.interactable = true;
	}
}
