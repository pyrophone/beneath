using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : AbstractCanvas
{
    [SerializeField]
    private Slider gains;

    [SerializeField]
    private Text maxXP;

    [SerializeField]
    private GameObject accompsAndBadges;

    [SerializeField]
    private Button returnButton;

    [SerializeField]
    private GameObject popUpContent;

    [SerializeField]
    private Button badgeButton;

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

        badgeButton.onClick.AddListener(OnBadgeClick);
        returnButton.onClick.AddListener(OnReturnToProfileButtonClick);

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
        lvlText.text = "Level:" + player.LVL; // get the player level
        expText.text = "xp: " + player.EXP; // get the player exp

        XPGains();
    }

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
		uiControl.SetCanvas(UIState.MAP);
	}

    private void OnBadgeClick()
    {
        accompsAndBadges.SetActive(false);
        popUpContent.SetActive(true);
    }

    private void OnReturnToProfileButtonClick()
    {
        accompsAndBadges.SetActive(true);
        popUpContent.SetActive(false);
    }

    /*! \brief Called when xp has been gained
     */
    private void XPGains()
    {
        /*
        switch (player.EXP)
        {
            case 100:
                if(player.LVL == 0)
                    gains.transform.localScale = new Vector3(0.3f, 1.0f, 1.0f);
                else if(player.LVL == 1)
                    gains.transform.localScale = new Vector3(0.15f, 1.0f, 1.0f);
                else if(player.LVL == 2)
                    gains.transform.localScale = new Vector3(0.09f, 1.0f, 1.0f);
                break;
        }
        */

        if(player.LVL == 0)
        {
            maxXP.text = "300 xp";
        }
        else if(player.LVL == 1)
        {
            maxXP.text = "600 xp";
        }
        else if(player.LVL == 2)
        {
            maxXP.text = "900 xp";
        }

        if(player.EXP == 100) // 100 XP checks
        {
            if(player.LVL == 0)
            {
                gains.transform.localScale = new Vector3(0.3f, 1.0f, 1.0f);
            }
            else if(player.LVL == 1)
            {
                gains.transform.localScale = new Vector3(0.15f, 1.0f, 1.0f);
            }
            else if (player.LVL == 2)
            {
                gains.transform.localScale = new Vector3(0.09f, 1.0f, 1.0f);
            }
        }
        else if(player.EXP == 200) // 200 XP checks
        {
            if (player.LVL == 0)
            {
                gains.transform.localScale = new Vector3(0.6f, 1.0f, 1.0f);
            }
            else if (player.LVL == 1)
            {
                gains.transform.localScale = new Vector3(0.35f, 1.0f, 1.0f);
            }
            else if (player.LVL == 2)
            {
                gains.transform.localScale = new Vector3(0.2f, 1.0f, 1.0f);
            }
        }
        else if(player.EXP == 300) // 300 XP checks
        {
            if (player.LVL == 0) // level 1 req met
            {
                gains.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                LevelUp();
            } 
            else if (player.LVL == 1)
            {
                gains.transform.localScale = new Vector3(0.5f, 1.0f, 1.0f);
            }
            else if (player.LVL == 2)
            {
                gains.transform.localScale = new Vector3(0.36f, 1.0f, 1.0f);
            }
        }
        else if(player.EXP == 400) // 400 XP checks
        {
            if (player.LVL == 1)
            {
                gains.transform.localScale = new Vector3(0.7f, 1.0f, 1.0f);
            }
            else if(player.LVL == 2)
            {
                gains.transform.localScale = new Vector3(0.45f, 1.0f, 1.0f);
            }
        }
        else if (player.EXP == 500) // 500 XP checks
        {
            if (player.LVL == 1)
            {
                gains.transform.localScale = new Vector3(0.85f, 1.0f, 1.0f);
            }
            else if(player.LVL == 2)
            {
                gains.transform.localScale = new Vector3(0.56f, 1.0f, 1.0f);
            }
        }
        else if(player.EXP == 600) // 600 XP checks
        {
            if(player.LVL == 1) // level 2 req met
            {
                gains.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                LevelUp();
            }
            else if(player.LVL == 2)
            {
                gains.transform.localScale = new Vector3(0.67f, 1.0f, 1.0f);
            }
        }
        else if(player.EXP == 700) // 700 xp checks
        {
            if(player.LVL == 2)
            {
                gains.transform.localScale = new Vector3(0.71f, 1.0f, 1.0f);
            }
        }
        else if(player.EXP == 800) // 800 xp checks
        {
            if(player.LVL == 2)
            {
                gains.transform.localScale = new Vector3(0.82f, 1.0f, 1.0f);
            }
        }
        else if(player.EXP == 900) // 900 xp checks
        {
            if(player.LVL == 2) // level 3 req met
            {
                gains.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                // LevelUp();
                player.LVL = 3;
            }
        }
    }

    /*! \brief Called when a new level has been gained
     */
    private void LevelUp()
    {
        player.LVL += 1;

        player.EXP = 0;

        gains.transform.localScale = new Vector3(0, 1, 1);
    }
}
