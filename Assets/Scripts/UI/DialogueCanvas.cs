using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \class DialogueCanvas
 *	\brief Handles dialogue display
 */
public class DialogueCanvas : AbstractCanvas
{
	private Text nameField; //! The name field of the canvas
	private Text dialogueField; //! The text field of the canvas
	private Button nextButton; //! The next button for the dialogue screen
	private int convoNum; //! Progress in dialogue
	private int dialogueAmount; //! The amount of dialogue in each part
	private bool lastDialogue; //! If the dialogue is the last one

	/*! \brief Called on startup
	 */
	protected override void Awake()
	{
		base.Awake();

		nameField = transform.Find("Name").GetComponent<Text>();
		dialogueField = transform.Find("Text").GetComponent<Text>();
		nextButton = transform.Find("Button").GetComponent<Button>();
		nextButton.onClick.AddListener(OnButtonClick);
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

		if(convoNum > dialogueAmount - 1)
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

	/*! \brief Resets the dialogue progress
	 */
	public void ResetDialogue()
	{
		convoNum = 0;
	}

	/*! \brief Getter / Setter for nameField
	 */
	public Text NameField
	{
		get { return nameField; }
		set { nameField = value; }
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
}
