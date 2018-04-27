using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \class DialogueCanvas
 *	\brief Handles dialogue display
 */
public class DialogueCanvas : AbstractCanvas
{
	private Text header; //! The headerof the canvas
	private Text dialogueField; //! The text field of the canvas
	private Image bg; //! The background image
	private Image charPic; //! The background image
	private Button panel; //! The next button for the dialogue screen
	private Button rwdButton; //! The reward button
	private Button exitButton; //! The reward button
	private int convoNum; //! Progress in dialogue
	private int dialogueAmount; //! The amount of dialogue in each part
	private bool lastDialogue; //! If the dialogue is the last one
	private bool displayReward; //! If the reward should be displayed

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		//header = transform.Find("Header").Find("Text").GetComponent<Text>();
		dialogueField = transform.Find("Button").Find("Text").GetComponent<Text>();
		bg = transform.Find("BG").GetComponent<Image>();
		charPic = dialogueField.gameObject.transform.Find("Char").GetComponent<Image>();
		panel = transform.Find("Button").GetComponent<Button>();
		panel.onClick.AddListener(OnButtonClick);
		exitButton = panel.gameObject.transform.Find("Text").Find("Button").GetComponent<Button>();
		exitButton.onClick.AddListener(OnButtonClick);
		exitButton.gameObject.SetActive(false);
		rwdButton = panel.gameObject.transform.Find("Text").Find("rwdButton").GetComponent<Button>();
		rwdButton.onClick.AddListener(OnRWDButton);

		ResetDialogue();
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

	/*! \brief Event for when the next button is clicked
	 */
	private void OnButtonClick()
	{
		convoNum++;

		if(convoNum > dialogueAmount - 2)
		{
			if(!displayReward)
			{
				rwdButton.gameObject.SetActive(false);
				exitButton.gameObject.SetActive(true);
			}

			else
			{
				rwdButton.gameObject.SetActive(true);
				exitButton.gameObject.SetActive(false);
			}
		}

		else if(convoNum > dialogueAmount - 1)
		{
			convoNum = 0;
			uiControl.SetCanvas(UIState.MAP);
		}

		if(lastDialogue)
		{
			ResetDialogue();
			lastDialogue = false;
		}
	}

	/*! \brief Event for when the rewards button is used
	 */
	private void OnRWDButton()
	{
		convoNum = 0;
		uiControl.SetCanvas(UIState.PLAYER);
	}

	/*! \brief Resets the dialogue progress
	 */
	public void ResetDialogue()
	{
		convoNum = 0;
	}

	/*! \brief Getter / Setter for the background
	 */
	public Image BG
	{
		get { return BG; }
		set { BG = value; }
	}

	/*! \brief Getter / Setter for charPic
	 */
	public Image CharPic
	{
		get { return charPic; }
		set { charPic = value; }
	}

	/*! \brief Getter / Setter for dialogueField
	 */
	public Text DialogueField
	{
		get { return dialogueField; }
		set { dialogueField = value; }
	}

	/*! \brief Getter for convoNum
	 */
	public int ConvoNum
	{
		get { return convoNum; }
	}

	/*! \brief Setter for dialogueAmount
	 */
	public int DialogueAmount
	{
		set { dialogueAmount = value; }
	}

	/*! \brief Getter / Setter for the last dialogue bool
	 */
	public bool LastDialogue
	{
		get { return lastDialogue; }
		set { lastDialogue = value; }
	}

	/*! \brief Getter / Setter for the last dialogue bool
	 */
	public bool DisplayReward
	{
		get { return displayReward; }
		set { displayReward = value; }
	}
}
