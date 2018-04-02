using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MapCanvas : MonoBehaviour
{
	private UIControl uiControl; //! The UIControl component
	private Button qListButton; //! The button for the quest list screen
	private Button playerButton; //! The next button for the player screen
	private Button settingsButton; //! The next button for the settings screen
    private Button locButton; //! the button to copy the current location

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		uiControl = transform.parent.GetComponent<UIControl>();

		qListButton = transform.Find("QuestListButton").GetComponent<Button>();
		qListButton.onClick.AddListener(OnQListButtonClick);
		playerButton = transform.Find("PlayerButton").GetComponent<Button>();
		playerButton.onClick.AddListener(OnPlayerButtonClick);
		settingsButton = transform.Find("SettingsButton").GetComponent<Button>();
		settingsButton.onClick.AddListener(OnSettingsButtonClick);
        locButton = transform.Find("GeoButton").GetComponent<Button>();
        locButton.onClick.AddListener(OnGeoClick);
    }

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

    /*! \brief Called when the location text is clicked
	 */
    private void OnGeoClick()
    {
        //solution from https://github.com/sanukin39/UniClipboard    
        UniClipboard.SetText(transform.Find("GeoCounter").GetComponent<Text>().text);
    }

    /*! \brief Called when the quest list button is clicked
	 */
    private void OnQListButtonClick()
	{
		uiControl.SetCanvas(UIState.QLIST);
	}

	/*! \brief Called when the player button is clicked
	 */
	private void OnPlayerButtonClick()
	{
		uiControl.SetCanvas(UIState.PLAYER);
	}

	/*! \brief Called when the settings button is clicked
	 */
	private void OnSettingsButtonClick()
	{
		uiControl.SetCanvas(UIState.SETTINGS);
		uiControl.SettingsSwitchTo = UIState.MAP;
	}
}
