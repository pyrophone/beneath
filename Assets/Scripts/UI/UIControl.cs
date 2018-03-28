using System.Collections;
using System.Collections.Generic;

using Mapbox.Unity.Map;

using UnityEngine;
using UnityEngine.UI;

public enum UIState { INTRO, MAP, DIALOGUE, QLIST, PLAYER, SETTINGS};

/*! \class UIControl
 *	\brief Manages UI
 */
public class UIControl : MonoBehaviour
{
	[SerializeField]
	private GameObject[] canvases; //! Canvases to switch between
	[SerializeField]
	private GameObject mapObj; //! Reference to the map
	private int curDialogue; //! The current dialogue
	private DialogueCanvas dial; //! The dialogue canvas script
	private QListCanvas qlCanvas; //! The quest list canvas script
	private UIState currentUIState; //! The current state of the UI
	private UIState settingsSwitchTo; //! The UIState tha the settings menu should switch to

	private void Awake()
	{
		qlCanvas = transform.Find("QuestCanvas").GetComponent<QListCanvas>();
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

		dial = transform.Find("DialogCanvas").GetComponent<DialogueCanvas>();
		//qlCanvas = transform.Find("QuestCanvas").GetComponent<QListCanvas>();
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
		canvases[(int)currentUIState].SetActive(false);
		currentUIState = newState;
		canvases[(int)currentUIState].SetActive(true);

		if(currentUIState == UIState.MAP)
			mapObj.SetActive(true);

		else
			mapObj.SetActive(false);
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
}
