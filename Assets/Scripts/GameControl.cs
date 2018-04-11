using System.Collections;
using System.Collections.Generic;

using Mapbox.Unity.Map;
using Mapbox.Examples;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*! \class GameControl
 *	\brief Acts as a general game manager
 */
public class GameControl : MonoBehaviour
{
	[SerializeField]
	private GameObject playerPrefab; //! The prefab for the player

    [SerializeField]
    private QuadTreeCameraMovement mapCam; //! The map camera control

    [SerializeField]
	private AbstractMap map; //! The map
	private QControl qControl; //! Reference to the quest controller
	private UIControl uiControl; //! Reference to the UI controller
	private GameObject player; //! The player object
	private bool doTutorial; //! If the player should run through the tutorial
	private bool updatePlayer; //! If the player data should update

    // Settings
    [SerializeField]
    public bool Debug; //! Debug bool (on/off) currently used for distance

    /*! \brief Called when the game is initialized (ensures this code runs first no matter what)
	 */
    private void Awake()
    {
		DontDestroyOnLoad(this);
		player = (GameObject)Instantiate(playerPrefab);
		player.name = "player";
		player.GetComponent<Player>().Map = this.map;
		Player.pName = "player";
		DontDestroyOnLoad(player);

		qControl = GetComponent<QControl>();
		uiControl = GetComponent<UIControl>();

		//doTutorial = true;
		uiControl.TutorialActive = true;
    }

    /*! \brief Called when the object is initialized
	 */
    private void Start()
	{

	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
        //set player as map center
        mapCam.CenterOnTarget(player.GetComponent<Player>().Loc);

        //for now, this will test vuforia by switching the scene on click or tap.
        if (Input.GetMouseButtonDown(0) && Input.touchCount > 2)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(1);
            else
                SceneManager.LoadScene(0);
        }

        if(updatePlayer)
        {
			UpdatePlayerInfo();
			updatePlayer = false;
        }

        if (qControl.CurQuest != null)
        {
			switch(uiControl.CurrentUIState)
			{
				case UIState.DIALOGUE:
					if(qControl.MarkerCurrent < qControl.CurQuest.dialogueNum.Count - 1)
					{
						uiControl.Dial.DialogueAmount = qControl.CurQuest.convo[qControl.MarkerCurrent].convoPiece.Count;
						uiControl.Dial.NameField.text = qControl.CurQuest.convo[qControl.MarkerCurrent].name;
						string text = qControl.CurQuest.convo[qControl.MarkerCurrent].convoPiece[uiControl.Dial.ConvoNum].Replace("-----", Player.pName);
						uiControl.Dial.DialogueField.text = text;
					}

					if(qControl.QuestShouldFinish)
					{
						uiControl.Dial.DialogueAmount = qControl.CurQuest.convo[qControl.CurQuest.convo.Count - 1].convoPiece.Count;
						uiControl.Dial.LastDialogue = true;
						uiControl.Dial.NameField.text = qControl.CurQuest.convo[qControl.CurQuest.convo.Count - 1].name;
						uiControl.Dial.DialogueField.text = "Reward: " + qControl.CurQuest.reward;
						qControl.SetCurrentQuest(null);
						qControl.QuestShouldFinish = false;
					}

					break;

				default:
					break;
			}
        }

	}

	/*! \brief Updates the player info on other screens
	 */
	public void UpdatePlayerInfo()
	{
		Player.pName = uiControl.PName;
	}

	/*! \brief Gets the map data
	 *
	 * \return (AbstractMap) The map data
	 */
	public AbstractMap Map
	{
		get { return this.map; }
	}

	/*! \brief Getter / Setter for updatePData bool
	 */
	public bool UpdatePlayer
	{
		get { return updatePlayer; }
		set { updatePlayer = value; }
	}

	/*! \brief Gets the player prefab
	 */
	public GameObject PlayerPrefab
	{
		get { return playerPrefab; }
	}
}
