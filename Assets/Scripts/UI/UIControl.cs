using System.Collections;
using System.Collections.Generic;

using Mapbox.Unity.Map;

using UnityEngine;
using UnityEngine.UI;

public enum UIState { MAP, DIALOGUE, QLIST, PLAYER, SETTINGS };

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
	private DialogueCanvas dial; //! The dialogue helper
	private UIState currentUIState; //! The current state of the UI

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{

		foreach(GameObject c in canvases)
		{
			c.SetActive(false);
		}

		currentUIState = UIState.MAP;
		SetCanvas(currentUIState);

		dial = transform.Find("DialogCanvas").GetComponent<DialogueCanvas>();
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

	public void PopulateQuestList()
	{

	}

	/*! \brief Getter / setter for the dialogue of the ui
	 */
	public DialogueCanvas Dial
	{
		get { return dial; }
		set { dial = value; }
	}

	/*! \brief Getter / setter for the current UI state
	 */
	public UIState CurrentUIState
	{
		get { return currentUIState; }
		set { currentUIState = value; }
	}
}
