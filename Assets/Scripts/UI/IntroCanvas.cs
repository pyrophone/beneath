using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class IntroCanvas : MonoBehaviour
{
	private UIControl uiControl; //! Reference to the UI control
	private Button settingsButton; //! Reference to the settings button
	private Button playButton; //! Reference to the play button

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		uiControl = transform.parent.GetComponent<UIControl>();

		playButton = transform.Find("PlayButton").GetComponent<Button>();
		playButton.onClick.AddListener(OnPlayButtonClick);

		settingsButton = transform.Find("SettingsButton").GetComponent<Button>();
		settingsButton.onClick.AddListener(OnSettingsButtonClick);
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Called when the play button is clicked
	 */
	private void OnPlayButtonClick()
	{
		uiControl.SetCanvas(UIState.MAP);
	}

	/*! \brief Called when the settings button is clicked
	 */
	private void OnSettingsButtonClick()
	{
		uiControl.SetCanvas(UIState.SETTINGS);
		uiControl.SettingsSwitchTo = UIState.INTRO;
	}
}
