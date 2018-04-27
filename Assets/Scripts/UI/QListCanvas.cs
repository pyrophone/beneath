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
	private GameObject avScrollContent; //! Scroll content list
	private GameObject coScrollContent; //! Scroll content list
	[SerializeField]
	private GameObject tglObj; //! Used to instantiate buttons for the scroll view.
	[SerializeField]
	private GameObject compObj; //! Used to instantiate buttons for the scroll view.
	private GameObject popUpPanel; //! The panel that pops up for confirmations

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		avScrollContent = transform.Find("AvailableScrollView").Find("Viewport").Find("Content").gameObject;
		coScrollContent = transform.Find("CompletedScrollView").Find("Viewport").Find("Content").gameObject;
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
		uiControl.SetCanvas(UIState.MAP);
	}

	/*! \brief Refreshes the quest list
	 *
	 * \param (Dictionary<Quest, bool>) quests - Dictionary of the available quests
	 * \param (Quest) current - The current quest
	 */
	public void RefreshQuestList(Dictionary<Quest, bool> quests, Quest current)
	{
		Vector3 offset1 = new Vector3(0.0f, 75.0f, 0.0f);
		Vector3 offset2 = new Vector3(0.0f, 75.0f, 0.0f);

		for(int i = avScrollContent.transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(avScrollContent.transform.GetChild(i).gameObject);
		}

		for(int i = coScrollContent.transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(coScrollContent.transform.GetChild(i).gameObject);
		}

		foreach(var quest in quests)
		{
			if(quest.Value == false)
			{
				GameObject tgl = Instantiate(tglObj);
				tgl.transform.SetParent(avScrollContent.transform, false);
				tgl.transform.Find("Toggle").Find("Label").GetComponent<Text>().text = quest.Key.name;
				tgl.transform.localPosition += offset1;

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

				offset1 -= new Vector3(0, 150.0f, 0.0f);
			}

			else
			{
				GameObject txt = Instantiate(compObj);
				txt.transform.SetParent(coScrollContent.transform, false);
				txt.GetComponent<Text>().text = quest.Key.name;
				txt.transform.localPosition += offset2;

				offset2 -= new Vector3(0, 150.0f, 0.0f);
			}
		}

		for(int i = 0; i < avScrollContent.transform.childCount; i++)
		{
			Transform child = avScrollContent.transform.GetChild(i);
		}
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
				popUpPanel.SetActive(false);
			});

		if(tgl.isOn)
		{
			popUpPanel.transform.Find("Header").Find("Title").GetComponent<Text>().text = q.name;
			popUpPanel.transform.Find("TextBox").Find("QuestDescriptionText").GetComponent<Text>().text = "Quest Description";
			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "Accept Quest?";
			popUpPanel.SetActive(true);

			yBtn.interactable = true;

			yBtn.onClick.AddListener(() => {
				qControl.SetCurrentQuest(q);
				popUpPanel.SetActive(false);

				//More funky anonymous function stuff
				if(uiControl.TutorialActive)
					transform.Find("../TutorialOverlay").GetComponent<TutorialOverlay>().SpecialClick();

				uiControl.SetCanvas(UIState.MAP);
			});

			if(uiControl.TutorialActive)
				nBtn.interactable = false;

			else
				nBtn.interactable = true;
		}

		else
		{
			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "Cancel Quest?";
			popUpPanel.SetActive(true);

			yBtn.interactable = true;
			yBtn.onClick.AddListener(() => { qControl.SetCurrentQuest(null); popUpPanel.SetActive(false); });

			uiControl.Dial.ResetDialogue();
		}
	}
}
