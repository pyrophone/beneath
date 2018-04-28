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
    private Button vibrateButton; //! Button to control Debug Level

    [SerializeField]
    private Button logOutButton; //! Button to logout

    [SerializeField]
    private Button replayTutorialButton; //! Button to replay tutorial

    [SerializeField]
    private Button distIndicButton;

    [SerializeField]
    private Sprite enabledTog;

    [SerializeField]
    private Sprite disabledTog;

	/*! \brief Called when the object is initialized
	 */
	protected override void Awake()
	{
		base.Awake();

		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);

        vibrateButton.onClick.AddListener(OnDebugButtonClick);
        distIndicButton.onClick.AddListener(OnDistanceIndicatorButtonClick);

        logOutButton.onClick.AddListener(OnLogOutButtonClick);
        replayTutorialButton.onClick.AddListener(OnReplayTutorialButtonClick);
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
		uiControl.SetCanvas(UIState.MAP);
	}

    /*! \brief Called when the vibe setting is toggled
	 */
    private void OnDebugButtonClick()
    {
        GameControl g = GameObject.Find("GameManager").GetComponent<GameControl>();

        if (!g.VibrateEnable)
        {
            // debugButton.GetComponent<Image>().color = new Color(.2f, 1, .2f);
            vibrateButton.GetComponentInChildren<Image>().sprite = enabledTog; // switches the image to enabled sprite
            g.VibrateEnable = true;
        }
        else
        {
            // debugButton.GetComponent<Image>().color = new Color(1, .2f, .2f);
            vibrateButton.GetComponentInChildren<Image>().sprite = disabledTog; // switches the image to disabled sprite
            g.VibrateEnable = false;
        }
    }

    /*! \brief Called when the distance indicator setting is toggled
     */
    private void OnDistanceIndicatorButtonClick()
    {
        GameControl g = GameObject.Find("GameManager").GetComponent<GameControl>();

        if (g.DistCountEnabled)
        {
            distIndicButton.GetComponentInChildren<Image>().sprite = disabledTog;
            g.DistCountEnabled = false;
        }
        else
        {
            distIndicButton.GetComponentInChildren<Image>().sprite = enabledTog;
           g.DistCountEnabled = true;
        }
    }

    /*! \brief Called when the logout button is clicked
     */
    private void OnLogOutButtonClick()
    {
        uiControl.SetCanvas(UIState.INTRO); // loads the intro canvas
        uiControl.TutorialActive = true; // reactivates the tutorial
    }

    /*! \brief Called when the replay tutorial button is clicked
     */
    private void OnReplayTutorialButtonClick()
    {
        uiControl.SetCanvas(UIState.TUTORIAL); // loads the tutorial canvas
        uiControl.TutorialActive = true; // reactivates the tutorial
    }
}
