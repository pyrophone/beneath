using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : AbstractCanvas
{
	private Button backButton; //! Reference to the back button

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

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

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
		uiControl.SetCanvas(uiControl.SettingsSwitchTo);
	}
}
