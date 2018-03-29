using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour
{
	private UIControl uiControl; //! Reference to the UI controller
	private Button backButton; //! Reference to the back button

    // Settings Button
    [SerializeField]
    private Button debugButton; //! Button to control Debug Level


	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		uiControl = transform.parent.GetComponent<UIControl>();
		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);
        debugButton.onClick.AddListener(OnDebugButtonClick);
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Called when the back button is clicked
	 */
	private void OnBackButtonClick()
	{
		uiControl.SetCanvas(uiControl.SettingsSwitchTo);
	}

    /*! \brief Called when the debug setting is toggled
	 */
    private void OnDebugButtonClick()
    {
        GameControl g = GameObject.Find("GameManager").GetComponent<GameControl>();
        g.Debug = !g.Debug;
    }
}
