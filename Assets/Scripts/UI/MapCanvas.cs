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

	// Use this for initialization
	private void Start()
	{
		uiControl = transform.parent.GetComponent<UIControl>();

		qListButton = transform.Find("QuestListButton").GetComponent<Button>();
		qListButton.onClick.AddListener(OnQListButtonClick);
		playerButton = transform.Find("PlayerButton").GetComponent<Button>();
		playerButton.onClick.AddListener(OnPlayerButtonClick);
		settingsButton = transform.Find("SettingsButton").GetComponent<Button>();
		settingsButton.onClick.AddListener(OnSettingsButtonClick);
	}

	// Update is called once per frame
	private void Update()
	{

	}

	private void OnQListButtonClick()
	{
		uiControl.SetCanvas(UIState.QLIST);
	}

	private void OnPlayerButtonClick()
	{
		uiControl.SetCanvas(UIState.PLAYER);
	}

	private void OnSettingsButtonClick()
	{
		uiControl.SetCanvas(UIState.SETTINGS);
	}
}
