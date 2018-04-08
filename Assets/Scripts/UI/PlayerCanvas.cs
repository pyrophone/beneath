using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*! \class PlayerCanvas
 *	\brief Handles the player canvas
 */
public class PlayerCanvas : AbstractCanvas
{
	private Button backButton; //! Reference to the back button

    private Text nameText;  //! The Text component related to the player's name
    private Text lvlText;   //! The Text component related to the player's level
    private Text expText;   //! The Text component related to the player's experience

    private Player player; //! The instantiated prefab of Player

    /*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);

        nameText = transform.Find("PlayerName").GetComponent<Text>();
        lvlText = transform.Find("PlayerLevel").GetComponent<Text>();
        expText = transform.Find("PlayerXP").GetComponent<Text>();

        GameObject gObj = GameObject.Find("GameManager");
        GameControl gc = gObj.GetComponent<GameControl>();
        gObj = gc.PlayerPrefab;
        player = gObj.GetComponent<Player>();
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
        nameText.text = player.GetName();
        lvlText.text = "LVL:" + player.GetLvl();
        expText.text = "EXP: " + player.GetExp();
    }

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
		uiControl.SetCanvas(UIState.MAP);
	}
}
