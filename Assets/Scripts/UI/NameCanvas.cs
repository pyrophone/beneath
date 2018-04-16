using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*! \class NameCanvas
 *	\brief Handles name input canvas
 */
public class NameCanvas : AbstractCanvas
{
	private GameObject panel; //! The panel containing the other elements
	private GameObject errorText; //! The text to display when an error occures
	private InputField nameField; //! The field for the character name
	private Button enterBtn; //! The enter button

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		panel = transform.Find("Panel").gameObject;
		errorText = panel.transform.Find("Text").gameObject;
		enterBtn = panel.transform.Find("CloseButton").GetComponent<Button>();
		enterBtn.onClick.AddListener(OnCloseClick);
		nameField = panel.transform.Find("InputField").GetComponent<InputField>();
	}

	/*! \brief Updates the object
	 */
	protected override void Update()
	{

	}

	/*! \brief Called when the close button for the name is clicked
	 */
	private void OnCloseClick()
	{
		if(nameField.text.Length < 3 || nameField.text.Length > 12)
			errorText.SetActive(true);

		else
		{
			uiControl.PName = nameField.text;
			gameObject.SetActive(false);
			transform.parent.GetComponent<GameControl>().UpdatePlayer = true;
			transform.parent.Find("TutorialCanvas").GetComponent<TutorialCanvas>().SpecialClick();
		}
	}
}
