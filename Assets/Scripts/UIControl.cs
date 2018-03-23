using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState { MAP, DIALOGUE };

/*! \class UIControl
 *	\brief Manages UI
 */
public class UIControl : MonoBehaviour
{
	[SerializeField]
	private GameObject[] canvases; //! Canvases to switch between
    [SerializeField]
    private int currentCanvas; //! The current scene being displayed
	private int curDialogue; //! The current dialogue
	private Dialogue dial; //! The dialogue helper
	private UIState currentUIState; //! The current state of the UI

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		currentCanvas = 0;

		foreach(GameObject c in canvases)
		{
			c.SetActive(false);
		}
        //ensure map is active (test)
        canvases[0].SetActive(true);
		dial = transform.Find("DialogCanvas").GetComponent<Dialogue>();
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Getter / Setter for currentScene
	 */
	public int CurrentCanvas
	{
		get { return currentCanvas; }
		set { currentCanvas = value; } //Could change this to do some varification of the value
	}

	/*! \brief Sets the canvas
	 */
	public void SetCanvas(UIState newState)
	{
		currentUIState = newState;
		canvases[currentCanvas].SetActive(false);
		currentCanvas = (int)currentUIState;
		canvases[currentCanvas].SetActive(true);
	}

	public void PopulateQuestList()
	{

	}

	public Dialogue Dial
	{
		get { return dial; }
		set { dial = value; }
	}

	public UIState CurrentUIState
	{
		get { return currentUIState; }
		set { currentUIState = value; }
	}

//	public void SetNextMarker()
//	{
//
//	}
}
