using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MapCanvas : AbstractCanvas
{
	// private UIControl uiControl; //! The UIControl component
	private Button qListButton; //! The button for the quest list screen
	private Button playerButton; //! The next button for the player screen
	private Button settingsButton; //! The next button for the settings screen
    private Button locButton; //! the button to copy the current location

    private Text nameText;  //! The Text component related to the player's name
    private Text lvlText;   //! The Text component related to the player's level
    private Text expText;   //! The Text component related to the player's experience

    private Player player; //! The instantiated prefab of Player

    /*! \brief Called when the object is initialized
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
        locButton = transform.Find("GeoButton").GetComponent<Button>();
        locButton.onClick.AddListener(OnGeoClick);

        nameText = playerButton.transform.Find("PlayerName").GetComponent<Text>();
        lvlText = playerButton.transform.Find("PlayerLevel").GetComponent<Text>();
        expText = playerButton.transform.Find("PlayerXP").GetComponent<Text>();

        /*
        nameText = transform.Find("PlayerName").GetComponent<Text>();
        lvlText = transform.Find("PlayerLevel").GetComponent<Text>();
        expText = transform.Find("PlayerXP").GetComponent<Text>();
        */
    }

    private void Start()
	{
        /*
		// uiControl = transform.parent.GetComponent<UIControl>();
        
        if(playerButton != null)
        {
            nameText =  playerButton.transform.Find("PlayerName").GetComponent<Text>();
            lvlText =   playerButton.transform.Find("PlayerLevel").GetComponent<Text>();
            expText =   playerButton.transform.Find("PlayerXP").GetComponent<Text>();
        }
        */
        player = GameObject.Find("player").GetComponent<Player>();
        
    }

	/*! \brief Updates the object
	 */
	protected override void Update()
	{
        nameText.text = player.PName; // get the player name
        lvlText.text = "LVL: " + player.LVL; // get the player level
        expText.text = "EXP: " + player.EXP; // get the player exp
    }

    /*! \brief Called when the location text is clicked
	 */
    private void OnGeoClick()
    {
        //solution from https://github.com/sanukin39/UniClipboard    
        UniClipboard.SetText(transform.Find("GeoCounter").GetComponent<Text>().text);
        Handheld.Vibrate();
    }

    /*! \brief Updates the ui for the tutorial
	 */
    public override void UpdateTutorialUI()
    {
        if (uiControl.TutorialActive)
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
        if (uiControl.TutorialActive)
        {
            TutorialOverlay to = transform.Find("../TutorialOverlay").GetComponent<TutorialOverlay>();
            if (to.TutorialProgress == 2)
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
