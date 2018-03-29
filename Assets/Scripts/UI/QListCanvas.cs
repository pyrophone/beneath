using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class QListCanvas : MonoBehaviour
{
	private UIControl uiControl; //! Reference to the UI controller
	private QControl qControl; //! Reference to the Quest controller
	private Button backButton; //! Reference to the back button
	private Button activeQuestButton; //! Reference to the active quest button
	private GameObject scrollContent; //! Scroll content list
	[SerializeField]
	private GameObject btnObj; //! Used to instantiate buttons for the scroll view.
	private GameObject popUpPanel; //! The panel that pops up for confirmations

	/*! \brief Called on startup
	 */
	private void Awake()
	{
		scrollContent = transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
		popUpPanel = transform.Find("PopUpPanel").gameObject;
		popUpPanel.SetActive(false);
		qControl = transform.parent.GetComponent<QControl>();
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		uiControl = transform.parent.GetComponent<UIControl>();
		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);

		activeQuestButton = transform.Find("ActiveQuestButton").GetComponent<Button>();
		activeQuestButton.onClick.AddListener(OnActiveButtonClick);
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
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
		}
	}

	/*! \brief Getter / Setter for the active quest button
	 */
	public void SetActiveQuestText(string text)
	{
		//Unity is ugly
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

		for(int i = 0; i < scrollContent.transform.childCount; i++)
		{
			Destroy(scrollContent.transform.GetChild(i).gameObject);
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

		if(prereq == null || prereq.completed == true)
		{
			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "Are you sure you want to accept " + q.name + "? This will cancel your current quest";
			popUpPanel.SetActive(true);

			popUpPanel.transform.Find("YesButton").GetComponent<Button>().interactable = true;
			//More funky anonymous function stuff
			popUpPanel.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(() => { qControl.SetCurrentQuest(q); popUpPanel.SetActive(false); });
		}

		else
		{
			popUpPanel.transform.Find("Text").GetComponent<Text>().text = "You must complete " + prereq.name + " before starting this quest.";
			popUpPanel.SetActive(true);

			popUpPanel.transform.Find("YesButton").GetComponent<Button>().interactable = false;
		}
	}
}
