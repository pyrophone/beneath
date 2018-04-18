using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
	private UIControl uiControl; //! Reference to the UI Controller
	private Button backButton; //! Reference to the back button

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
		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);

        nameText = transform.Find("PlayerName").GetComponent<Text>();
        lvlText = transform.Find("PlayerLevel").GetComponent<Text>();
        expText = transform.Find("PlayerXP").GetComponent<Text>();

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
        lvlText.text = "LVL:" + player.GetLvl();
        expText.text = "EXP: " + player.GetExp();
        // expText.text = "EXP: " + (player.GetExp() + arPCon.GainXp());
        /*
        if (player.GetExp() == 0)
        {
            expText.text = "EXP: " + 0;
        }
        else
        {
            
        }
        */
    }

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
		uiControl.SetCanvas(UIState.MAP);
	}
}
