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
	private GameObject displayText; //! The text to display when an error occures
	private GameObject errorText; //! The text to display when an error occures
	private InputField nameField; //! The field for the character name
	private Button enterBtn; //! The enter button
	private Button yesBtn; //! The yes button
	private Button noBtn; //! The no button

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		panel = transform.Find("Panel").gameObject;
		displayText = panel.transform.Find("Display").gameObject;
		errorText = panel.transform.Find("Error").gameObject;
		enterBtn = panel.transform.Find("CloseButton").GetComponent<Button>();
		enterBtn.onClick.AddListener(OnCloseClick);
		yesBtn = panel.transform.Find("YesButton").GetComponent<Button>();
		yesBtn.onClick.AddListener(OnYesClick);
		noBtn = panel.transform.Find("NoButton").GetComponent<Button>();
		noBtn.onClick.AddListener(OnNoClick);
		nameField = panel.transform.Find("InputField").GetComponent<InputField>();

		gameObject.SetActive(false);
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
			displayText.GetComponent<Text>().text = "Are you sure your name is " + nameField.text + "?";
			nameField.gameObject.SetActive(false);
			errorText.SetActive(false);
			enterBtn.gameObject.SetActive(false);
			yesBtn.gameObject.SetActive(true);
			noBtn.gameObject.SetActive(true);
		}
	}

	/*! \brief Called when the close button for the name is clicked
	 */
	private void OnYesClick()
	{
		uiControl.PName = nameField.text;
		transform.parent.GetComponent<GameControl>().UpdatePlayerInfo();

		if(uiControl.TutorialActive)
			transform.parent.Find("TutorialCanvas").GetComponent<TutorialCanvas>().SpecialClick();

		gameObject.SetActive(false);

		ResetFields();
	}

	/*! \brief Called when the close button for the name is clicked
	 */
	private void OnNoClick()
	{
		ResetFields();
	}

	/*! \brief Resets the fields for the name form thing
	 */
	private void ResetFields()
	{
		displayText.GetComponent<Text>().text = "What is your name, friend?";
	 	errorText.SetActive(false);
	 	displayText.SetActive(true);
		nameField.gameObject.SetActive(true);
		enterBtn.gameObject.SetActive(true);
		yesBtn.gameObject.SetActive(false);
		noBtn.gameObject.SetActive(false);
	}
}
