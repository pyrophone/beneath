using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \class Dialogue
 *	\brief Handles dialogue display
 */
public class DialogueCanvas : MonoBehaviour
{
    [SerializeField]
    private QControl qControl; //! Reference to quest control
	private Text dialogueField; //! The text field of the canvas
	private Button nextButton; //! The next button for the dialogue screen
    [SerializeField] //for debug
    private int dialogueNum; //! Num to keep track of indvidiual parts of dialogue
    [SerializeField] //for debug
	private int convoNum; //! Progress in dialogue
	private int dialogueAmount; //! The amount of dialogue in each part
	private UIControl uiControl; //! The UI controller component
	private bool lastDialogue; //! If the dialogue is the last one
    private int lastConvoDropoff; //! the index to add to dialogue amount to get the correct starting point in the dialogue

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		uiControl = transform.parent.GetComponent<UIControl>();
		dialogueField = transform.Find("Text").GetComponent<Text>();
		nextButton = transform.Find("Button").GetComponent<Button>();
		nextButton.onClick.AddListener(OnButtonClick);
		dialogueNum = 0;
		convoNum = 0;
        lastConvoDropoff = 0;
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Event for when the next button is clicked
	 */
	private void OnButtonClick()
	{
		convoNum++;

		if(convoNum > dialogueAmount - 1 + lastConvoDropoff)
		{
			dialogueNum++;
            lastConvoDropoff = convoNum;
			uiControl.SetCanvas(UIState.MAP);

            if (lastDialogue)
            {
                ResetDialogue();
                lastDialogue = false;
                qControl.SetCurrentQuest(null);
                GameObject.Find("DistCounter").GetComponent<Text>().text = "";
            }
        }       
    }

	public void ResetDialogue()
	{
		dialogueNum = 0;
		convoNum = 0;
        lastConvoDropoff = 0;
    }

	/*! \brief Getter / Setter for dialogueField
	 */
	public Text DialogueField
	{
		get { return dialogueField; }
		set { dialogueField = value; }
	}

	/*! \brief Getter / Setter for dialogueNum
	 */
	public int DialogueNum
	{
		get { return dialogueNum; }
		set { dialogueNum = value; }
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
