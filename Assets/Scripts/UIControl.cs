using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \class UIControl
 *	\brief Manages UI
 */
public class UIControl : MonoBehaviour
{
	private string dialogueFile; //! Location of the dialogue file
	private GameObject[] canvases; //! Canvases to switch between
	private int currentScene; //! The current scene being displayed

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{

	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Getter / Setter for currentScene
	 */
	public int CurrentScene
	{
		get { return currentScene; }
		set { currentScene = value; } //Could change this to do some varification of the value
	}

	/*! \brief Sets the canvas
	 */
	public void SetCanvas()
	{

	}

	public void CheckButton()
	{

	}

	public void PopulateQuestList()
	{

	}

	public void SetNextMarker()
	{

	}
}
