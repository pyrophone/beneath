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
	private GameObject scrollContent; //! Scroll content list
	[SerializeField]
	private GameObject tglObj; //! Used to instantiate buttons for the scroll view.
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
			backButton.interactable = false;

		else
		{
			backButton.interactable = true;
			popUpPanel.transform.Find("NoButton").GetComponent<Button>().interactable = true;
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
			if(quest.Value == false)
			{
				GameObject tgl = Instantiate(tglObj);
				tgl.transform.SetParent(scrollContent.transform, false);
				tgl.transform.Find("Toggle").Find("Label").GetComponent<Text>().text = quest.Key.name;
				tgl.transform.localPosition += offset;

				Toggle tglComp = tgl.transform.Find("Toggle").GetComponent<Toggle>();

				if(quest.Key != current)
					tglComp.isOn = false;
				else
					tglComp.isOn = true;

				//Funky anonymous function stuff to pass a param to a method
				tglComp.onValueChanged.AddListener(delegate{ QuestButtonClick(tglComp, quest.Key); } );
				Quest prereq = qControl.GetQuest(quest.Key.prereqQuestID);

				if(prereq != null && prereq.completed == false)
					tglComp.interactable = false;

				offset -= new Vector3(0, 150.0f, 0.0f);
			}
		}

		for(int i = 0; i < scrollContent.transform.childCount; i++)
		{
			Transform child = scrollContent.transform.GetChild(i);
		}

		if(uiControl.TutorialActive && GameObject.Find("TutorialOverlay").GetComponent<TutorialOverlay>().TutorialProgress < 8)
			scrollContent.transform.GetChild(0).Find("Toggle").GetComponent<Toggle>().interactable = false;
	}

	/*! \brief Called when a quest button is clicked
	 *
	 * \param (Toggle) tgl - The toggle to check
	 * \param (Quest) q - The quest to try to switch
	 */
	public void QuestButtonClick(Toggle tgl, Quest q)
	{
		if(uiControl.TutorialActive)
			transform.Find("../TutorialOverlay").GetComponent<TutorialOverlay>().SpecialClick();

		Button yBtn = popUpPanel.transform.Find("YesButton").GetComponent<Button>();
		Button nBtn = popUpPanel.transform.Find("NoButton").GetComponent<Button>();

		yBtn.onClick.RemoveAllListeners();
		nBtn.onClick.RemoveAllListeners();

		nBtn.onClick.AddListener(() => {
				tgl.isOn = !tgl.isOn;
				gameObject.transform.Find("PopUpPanel").gameObject.SetActive(false);
			});

		if(tgl.isOn)
		{
			popUpPanel.transform.Find("YesButton").Find("Text").GetComponent<Text>().text = "Accept";
			popUpPanel.transform.Find("NoButton").Find("Text").GetComponent<Text>().text = "Cancel";

			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "Are you sure you want to accept " + q.name + "? This will cancel your current quest.";
			popUpPanel.SetActive(true);

			yBtn.interactable = true;

			//More funky anonymous function stuff
			if(uiControl.TutorialActive) {
				yBtn.onClick.AddListener(() => {
					qControl.SetCurrentQuest(q);
					popUpPanel.SetActive(false);
					transform.Find("../TutorialOverlay").GetComponent<TutorialOverlay>().SpecialClick();
				});

				yBtn.interactable = false;
				nBtn.interactable = false;
			}

			else {
				yBtn.onClick.AddListener(() => {
					qControl.SetCurrentQuest(q);
					popUpPanel.SetActive(false);
				});

				nBtn.GetComponent<Button>().interactable = true;
			}

		}

		else
		{
			popUpPanel.transform.Find("YesButton").Find("Text").GetComponent<Text>().text = "Yes";
			popUpPanel.transform.Find("NoButton").Find("Text").GetComponent<Text>().text = "No";

			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "Are you sure you want to cancel your current quest?";
			popUpPanel.SetActive(true);

			yBtn.interactable = true;
			yBtn.onClick.AddListener(() => { qControl.SetCurrentQuest(null); popUpPanel.SetActive(false); });

			uiControl.Dial.ResetDialogue();
		}
	}

	/*! \brief Activates grim beginnings for the tutorial
	 *
	 * \param (bool) state - If the toggle should be set
	 */
	public void SetGrimBeginnings(bool state)
	{
		if(uiControl.TutorialActive)
			scrollContent.transform.GetChild(0).Find("Toggle").GetComponent<Toggle>().interactable = state;
	}

	/*! \brief Sets the quest accept button for the tutorial
	 *
	 * \param (bool) state - If the button should be set
	 */
	public void SetQuestAccept(bool state)
	{
		if(uiControl.TutorialActive)
			popUpPanel.transform.Find("YesButton").GetComponent<Button>().interactable = state;
	}

	/*! \brief Activates the back button for the tutorial
	 *
	 * \param (bool) state - If the button should be set
	 */
	public void SetBackButton(bool state)
	{
		if(uiControl.TutorialActive)
			backButton.interactable = state;
	}
}
