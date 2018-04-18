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

    private Text nameText;  //! The Text component related to the player's name
    private Text lvlText;   //! The Text component related to the player's level
    private Text expText;   //! The Text component related to the player's experience

    private Player player; //! The instantiated prefab of Player

    private ARPuzzleControl arPCon;

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

        if(playerButton != null)
        {
            nameText =  playerButton.transform.Find("PlayerName").GetComponent<Text>();
            lvlText =   playerButton.transform.Find("PlayerLevel").GetComponent<Text>();
            expText =   playerButton.transform.Find("PlayerXP").GetComponent<Text>();
        }

        GameObject gObj = GameObject.Find("GameManager");
        GameControl gc = gObj.GetComponent<GameControl>();
        gObj = gc.playerPrefab;
        player = gObj.GetComponent<Player>();

        GameObject temp = GameObject.Find("coin_05");
        arPCon = temp.GetComponent<ARPuzzleControl>();
    }

	/*! \brief Updates the object
	 */
	private void Update()
	{
        nameText.text = player.GetName();
        lvlText.text = "LVL: " + player.GetLvl();
        expText.text = "EXP: " + player.GetExp();
        
        /*
        if (player.GetExp() == 0)
        {
            expText.text = "EXP: " + 0;
        }
        else
        {
            expText.text = "EXP: " + (player.GetExp() + arPCon.GainXp());
        }
        */
    }

    /*! \brief Called when the location text is clicked
	 */
    private void OnGeoClick()
    {
        //solution from https://github.com/sanukin39/UniClipboard    
        UniClipboard.SetText(transform.Find("GeoCounter").GetComponent<Text>().text);
        Handheld.Vibrate();
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

    /*
    public void AddXP()
    {
        expText.text = "EXP: " + (player.GetExp() + arPCon.GainXp());
    }
    */
}
