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

    Dictionary<string, Sprite> sprites;

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

		sprites = new Dictionary<string, Sprite>();

		Object[] bgs = Resources.LoadAll("Backgrounds", typeof(Sprite));
		Object[] chars = Resources.LoadAll("Characters", typeof(Sprite));

		foreach(Sprite s in bgs)
		{
			sprites.Add("Backgrounds/" + s.name, s);
		}

		foreach(Sprite s in chars)
		{
			sprites.Add("Characters/" + s.name, s);
		}
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

                uiControl.SetCanvas(UIState.VUFORIA); // sets the canvas to the vuforia canvas

                for (int i = 0; i < items.Count; i++)
                {
                    items[i].SetActive(true); // makes sure that all items are active when switching to AR camera view
                }
            }

            else
            {
                cams[0].enabled = true; // enables the main camera view
                cams[1].enabled = false; // disables the AR camera view

                uiControl.SetCanvas(UIState.MAP); // sets the canvas to the map canvas
            }
        }

        if (qControl.CurQuest != null)
        {
			switch(uiControl.CurrentUIState)
			{
				case UIState.DIALOGUE:
                    if (qControl.MarkerCurrent < qControl.CurQuest.dialogueNum.Count - 1)
                    {
                        uiControl.Dial.DialogueAmount = qControl.CurQuest.convo[qControl.MarkerCurrent].convoPiece.Count;
                        string text = qControl.CurQuest.convo[qControl.MarkerCurrent].convoPiece[uiControl.Dial.ConvoNum].Replace("-----", player.GetComponent<Player>().PName);
                        uiControl.Dial.DialogueField.text = text;

                        Sprite s = sprites[qControl.CurQuest.convo[qControl.MarkerCurrent].bgPic];
						//UnityEngine.Debug.Log(s);

                        //if(s != null)
						//	uiControl.Dial.BG.sprite = s;

						s =  sprites[qControl.CurQuest.convo[qControl.MarkerCurrent].charPic];

						//UnityEngine.Debug.Log(s);
						//if(s != null)
						//	uiControl.Dial.CharPic.sprite = s;
                    }

                    if (qControl.QuestShouldFinish)
                    {
                        uiControl.Dial.DialogueAmount = qControl.CurQuest.convo[qControl.CurQuest.convo.Count - 1].convoPiece.Count;
                        uiControl.Dial.LastDialogue = true;
                        //uiControl.Dial.DialogueField.text = "Reward: " + qControl.CurQuest.reward;
                        string text = qControl.CurQuest.convo[qControl.MarkerCurrent].convoPiece[uiControl.Dial.ConvoNum].Replace("-----", player.GetComponent<Player>().PName);
                        uiControl.Dial.DialogueField.text = text;
                        qControl.SetCurrentQuest(null);
                        qControl.QuestShouldFinish = false;
                    }
                    break;

				default:
					break;
			}
        }
	}

    public void UpdatePlayerInfo()
    {
        player.GetComponent<Player>().PName = uiControl.PName;
    }

	/*! \brief Gets the map data
	 *
	 * \return (AbstractMap) The map data
	 */
	public AbstractMap Map
	{
		get { return this.map; }
	}

    // getter / setter for playerprefab
    public GameObject PlayerPrefab
    {
        get { return playerPrefab; }
    }
}
