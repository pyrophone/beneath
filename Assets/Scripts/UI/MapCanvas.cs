using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MapCanvas : AbstractCanvas
{
	private Button qListButton; //! The button for the quest list screen
	private Button playerButton; //! The next button for the player screen
	private Button settingsButton; //! The next button for the settings screen

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

	 	qListButton = transform.Find("QuestListButton").GetComponent<Button>();
		qListButton.onClick.AddListener(OnQListButtonClick);
		playerButton = transform.Find("PlayerButton").GetComponent<Button>();
		playerButton.onClick.AddListener(OnPlayerButtonClick);
		settingsButton = transform.Find("SettingsButton").GetComponent<Button>();
		settingsButton.onClick.AddListener(OnSettingsButtonClick);
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
		{
			playerButton.interactable = false;
			settingsButton.interactable = false;
		}

		else
		{
			playerButton.interactable = true;
			settingsButton.interactable = true;
		}
	}

	/*! \brief Called when the quest list button is clicked
	 */
	private void OnQListButtonClick()
	{
		if(uiControl.TutorialActive)
		{
			TutorialOverlay to = transform.Find("../TutorialOverlay").GetComponent<TutorialOverlay>();
			if(to.TutorialProgress == 2)
			{
				to.SpecialClick();
				uiControl.SetCanvas(UIState.QLIST);
			}
		}

		else
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
