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
	private QControl qControl;
	private UIControl uiControl;
	private GameObject player; //! The player object

    // Settings
    public bool Debug; //! Debug bool (on/off)

    /*! \brief Called when the game is initialized (ensures this code runs first no matter what)
	 */
    private void Awake()
    {
		DontDestroyOnLoad(this);
		player = (GameObject)Instantiate(playerPrefab);
		player.GetComponent<Player>().Map = this.map;
		player.name = "player";
		DontDestroyOnLoad(player);

		qControl = GetComponent<QControl>();
		uiControl = GetComponent<UIControl>();
    }

    /*! \brief Called when the object is initialized
	 */
    private void Start()
	{
		//TODO: something that needs to go here everytime the scene is loaded
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

        if (qControl.CurQuest != null)
        {
			switch(uiControl.CurrentUIState)
			{
				//case UIState.MAP:
				//	break;

				case UIState.DIALOGUE:
					if(uiControl.Dial.DialogueNum < qControl.CurQuest.dialogueAmount.Count ||
						uiControl.Dial.ConvoNum < qControl.CurQuest.convo.Count)
					{
						uiControl.Dial.DialogueAmount = qControl.CurQuest.dialogueAmount[uiControl.Dial.DialogueNum];
						uiControl.Dial.DialogueField.text = qControl.CurQuest.convo[uiControl.Dial.ConvoNum];
					}

					if(qControl.QuestShouldFinish)
					{
						qControl.CurQuest = null;
						qControl.QuestShouldFinish = false;
					}
					break;

				default:
					break;
			}
        }
	}

	/*! \brief Gets the map data
	 *
	 * \return (AbstractMap) The map data
	 */
	public AbstractMap Map
	{
		get { return this.map; }
	}
}
