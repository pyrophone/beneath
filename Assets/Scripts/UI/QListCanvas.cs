using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class QListCanvas : MonoBehaviour
{
	private UIControl uiControl; //! Reference to the UI controller
	private Button backButton; //! Reference to the back button
	private Button activeQuestButton; //! Reference to the active quest button

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

	}

	/*! \brief Getter / Setter for the active quest button
	 */
	public void SetActiveQuestText(string text)
	{
		//Unity is ugly
		transform.Find("ActiveQuestButton").Find("Text").GetComponent<Text>().text = text;
	}
}
