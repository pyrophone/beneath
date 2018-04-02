using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
	private UIControl uiControl; //! Reference to the UI Controller
	private Button backButton; //! Reference to the back button

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		uiControl = transform.parent.GetComponent<UIControl>();
		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);
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
}
