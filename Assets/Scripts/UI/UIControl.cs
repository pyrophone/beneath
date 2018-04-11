using System.Collections;
using System.Collections.Generic;

using Mapbox.Unity.Map;

using UnityEngine;
using UnityEngine.UI;

public enum UIState { INTRO, MAP, DIALOGUE, QLIST, PLAYER, SETTINGS, TUTORIAL, PUZZLE, VUFORIA };

/*! \class UIControl
 *	\brief Manages UI
 */
public class UIControl : MonoBehaviour
{
	[SerializeField]
	private GameObject[] canvases; //! Canvases to switch between
	[SerializeField]
	private GameObject mapObj; //! Reference to the map
	private DialogueCanvas dial; //! The dialogue canvas script
	private QListCanvas qlCanvas; //! The quest list canvas script
	private UIState currentUIState; //! The current state of the UI
	private UIState settingsSwitchTo; //! The UIState tha the settings menu should switch to
	private bool doTutOverlay; //! If the tutorial overlay
    [SerializeField]
	private bool tutorialActive; //! Is the tutorial active
	private string pName; //! The name of the player

	/*! \brief Called on startup
	 */
	private void Awake()
	{
		qlCanvas = transform.Find("QuestCanvas").GetComponent<QListCanvas>();
		dial = transform.Find("DialogCanvas").GetComponent<DialogueCanvas>();
		tutorialActive = true;
		doTutOverlay = false;

	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		foreach(GameObject c in canvases)
		{
			c.SetActive(false);
		}

		currentUIState = UIState.INTRO;
		SetCanvas(currentUIState);
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Sets the canvas
	 */
	public void SetCanvas(UIState newState)
	{
		UpdateTutorial();

		canvases[(int)currentUIState].SetActive(false);
		currentUIState = newState;
		canvases[(int)currentUIState].SetActive(true);
	}

	/*! \brief Updates the tutorial info
	 */
	public void UpdateTutorial()
	{
		for(int i = 0; i < canvases.Length; i++)
			canvases[i].GetComponent<AbstractCanvas>().UpdateTutorialUI();

		transform.Find("TutorialOverlay").GetComponent<TutorialOverlay>().UpdateTutorialUI();
	}

	/*! \brief Getter / Setter for the dialogue canvas
	 */
	public DialogueCanvas Dial
	{
		get { return dial; }
		set { dial = value; }
	}

	/*! \brief Getter / Setter for the QustList canvas
	 */
	 public QListCanvas QLCanvas
	 {
		get { return qlCanvas; }
		set { qlCanvas = value; }
	 }

	/*! \brief Getter / Setter for the current UI state
	 */
	public UIState CurrentUIState
	{
		get { return currentUIState; }
		set { currentUIState = value; }
	}

	/*! \brief Getter / Setter for settingsSwitchTo
	 */
	public UIState SettingsSwitchTo
	{
		get { return settingsSwitchTo; }
		set { settingsSwitchTo = value; }
	}

	/*! \brief Getter / Setter for doTutOverlay
	 */
	public bool DoTutOverlay
	{
		get { return doTutOverlay; }
		set { doTutOverlay = value; }
	}

	/*! \brief Getter / Setter for doTutOverlay
	 */
	public bool TutorialActive
	{
		get { return tutorialActive; }
		set { tutorialActive = value; }
	}

	/*! \brief Getter for the player name
	 */
	public string PName
	{
		get { return pName; }
		set { pName = value; }
	}
}
