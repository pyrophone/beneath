using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : AbstractCanvas
{ 
	private Button backButton; //! Reference to the back button

    private Text nameText;  //! The Text component related to the player's name
    private Text lvlText;   //! The Text component related to the player's level
    private Text expText;   //! The Text component related to the player's experience

    private Player player; //! The instantiated prefab of Player

    protected override void Awake()
    {
        base.Awake();

        backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);

        nameText = transform.Find("PlayerName").GetComponentInChildren<Text>();
        lvlText = transform.Find("PlayerLevel").GetComponent<Text>();
        expText = transform.Find("PlayerXP").GetComponent<Text>();
    }

    /*! \brief Called when the object is initialized
	 */
    private void Start()
	{
        player = GameObject.Find("player").GetComponent<Player>();
    }

    /*! \brief Updates the object
	 */
    protected override void Update()
	{
        nameText.text = player.PName; // get the player name
        lvlText.text = "LVL:" + player.LVL; // get the player level
        expText.text = "EXP: " + player.EXP; // get the player exp
    }

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
		uiControl.SetCanvas(UIState.MAP);
	}
}
