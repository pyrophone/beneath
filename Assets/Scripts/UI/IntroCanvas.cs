using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*! \class IntroCanvas
 *	\brief Handles the intro canvas
 */
public class IntroCanvas : AbstractCanvas
{
	private Button playButton; //! Reference to the play button

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		playButton = transform.Find("PlayButton").GetComponent<Button>();
		playButton.onClick.AddListener(OnPlayButtonClick);
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{

	}

	/*! \brief Updates the object
	 */
	protected override void Update()
	{

	}

	/*! \brief Called when the play button is clicked
	 */
	private void OnPlayButtonClick()
	{
		uiControl.SetCanvas(UIState.TUTORIAL);
	}
}
