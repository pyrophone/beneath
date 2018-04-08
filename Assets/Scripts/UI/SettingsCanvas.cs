using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*! \class Settings
 *	\brief Handles settings canvas
 */
public class SettingsCanvas : AbstractCanvas
{
	private Button backButton; //! Reference to the back button

    // Settings Button
    [SerializeField]
    private Button debugButton; //! Button to control Debug Level

	/*! \brief Called when the object is initialized
	 */
	protected override void Awake()
	{
		base.Awake();

		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);
        debugButton.onClick.AddListener(OnDebugButtonClick);
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
        if (g.Debug)
        {
            debugButton.GetComponent<Image>().color = new Color(.2f, 1, .2f);
        }
        else
        {
            debugButton.GetComponent<Image>().color = new Color(1, .2f, .2f);
        }
    }
}
