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
        badgeButton.interactable = false;

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
        gains.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
    }

    /*! \brief Updates the object
	 */
    protected override void Update()
	{
        nameText.text = player.PName; // get the player name
        lvlText.text = "Level " + player.LVL; // get the player level
        expText.text = "xp " + player.EXP; // get the player exp

        XPGains();
    }

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
		OnReturnToProfileButtonClick();
		uiControl.SetCanvas(UIState.MAP);
	}

    public void OnBadgeClick()
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
        maxXP.text = LevelXPTop(player.LVL).ToString();

        float progress = (float)player.EXP / LevelXPTop(player.LVL);
        gains.transform.localScale = new Vector3(progress, 1.0f, 1.0f);

        if (player.EXP >= LevelXPTop(player.LVL))
            player.LVL++;
    }

    /*! \brief Called to get top amount of xp that a level has
     */
    private int LevelXPTop(int level)
    {
        return 100 * (level * level) + 600 * level + 300;
    }

    public void SetReward(string resource)
    {
		GameObject g = transform.Find("AccomplishmentsAndBadges").Find(resource).Find(resource).gameObject;
		g.GetComponent<Image>().sprite = ResourceManager.GetSprite("AchievementItems/" + resource);
		g.SetActive(true);
        badgeButton.interactable = true;
    }
}
