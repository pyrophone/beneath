using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VuforiaCanvas : AbstractCanvas
{
    [SerializeField]
    private GameObject popUp;

    [SerializeField]
    private Button helpButton;

    [SerializeField]
    private Button gotItButton;

    private Button backButton;

    // private Button tempObj;

    protected override void Awake()
    {
        base.Awake();

        backButton = transform.Find("BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(OnBackButtonClick);

        gotItButton.onClick.AddListener(OnGotItButtonClick);
        helpButton.onClick.AddListener(OnHelpButtonClick);
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
        GameObject.Find("GameManager").GetComponent<GameControl>().Cams[0].enabled = true; // enables the main camera view
        GameObject.Find("GameManager").GetComponent<GameControl>().Cams[1].enabled = false; // disables the AR camera view
        uiControl.SetCanvas(UIState.MAP);
    }

    private void OnHelpButtonClick()
    {
        popUp.SetActive(true);
        helpButton.interactable = false;
    }

    private void OnGotItButtonClick()
    {
        popUp.SetActive(false);
        helpButton.interactable = true;
    }

}
