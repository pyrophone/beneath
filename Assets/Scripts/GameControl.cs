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
	public GameObject playerPrefab; //! The prefab for the player

    [SerializeField]
    private QuadTreeCameraMovement mapCam; //! The map camera control

    [SerializeField]
	private AbstractMap map; //! The map

    [SerializeField]
    private Camera[] cams = new Camera[2]; // Array that holds the two cameras, "0" is the map camera view, "1" is the AR Puzzle camera view

    [SerializeField]
    private List<GameObject> items; // List that contains references to all of the coin prefabs that are children to the image targets

	private QControl qControl;
	private UIControl uiControl;
	private GameObject player; //! The player object

    // Settings
    [SerializeField]
    public bool Debug; //! Debug bool (on/off) currently used for distance

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
        cams[1].enabled = false; // makes sure the AR camera is not active when the application is started
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
        //set player as map center
        mapCam.CenterOnTarget(player.GetComponent<Player>().Loc);

        //for now, this will test vuforia by switching the scene on click, tap, or keypress.
        if (Input.GetMouseButtonDown(0) && Input.touchCount > 2 || Input.GetKeyDown(KeyCode.T))
        {
            if (cams[0].enabled == true)
            {
                cams[0].enabled = false; // disables the main camera view
                cams[1].enabled = true; // enables the AR camera view

                uiControl.canvases[1].SetActive(false); // turns off the map canvas content when using the AR camera
                // set the AR Puzzle canvas to be enabled when switching to the AR Puzzle view

                for (int i = 0; i < items.Count; i++)
                {
                    items[i].SetActive(true); // makes sure that all items are active when switching to AR camera view
                }
            }
            else
            {
                cams[0].enabled = true; // enables the main camera view
                cams[1].enabled = false; // disables the AR camera view
                uiControl.canvases[1].SetActive(true); // turns the map canvas back on
                // set the AR Puzzle canvas to be disabled when switching back to the map view
            }
            /*
            if (SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(1);
            else
                SceneManager.LoadScene(0);
                */
        }

        if (qControl.CurQuest != null)
        {
			switch(uiControl.CurrentUIState)
			{
				case UIState.DIALOGUE:
					if(uiControl.Dial.DialogueNum < qControl.CurQuest.dialogueAmount.Count ||
						uiControl.Dial.ConvoNum < qControl.CurQuest.convo.Count)
					{
						uiControl.Dial.DialogueAmount = qControl.CurQuest.dialogueAmount[uiControl.Dial.DialogueNum];
						uiControl.Dial.DialogueField.text = qControl.CurQuest.convo[uiControl.Dial.ConvoNum];
					}

					if(qControl.QuestShouldFinish)
					{
						uiControl.Dial.LastDialogue = true;
						//current quest should not be set to null until dialogue is finished
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
