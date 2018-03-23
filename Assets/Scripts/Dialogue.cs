using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \class Dialogue
 *	\brief Handles dialogue display
 */
public class Dialogue : MonoBehaviour
{
	private Text dialogueField; //! The text field of the canvas
	private Button nextButton; //! The next button for the dialogue screen
	private int dialogueNum; //! Num to keep track of indvidiual parts of dialogue
	private int convoNum; //! Progress in dialogue
	private int dialogueAmount; //! The amount of dialogue in each part
	private bool switchToMap; //! If the dialogue should switch back to the map

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		dialogueField = transform.Find("Text").GetComponent<Text>();
		nextButton = transform.Find("Button").GetComponent<Button>();
		nextButton.onClick.AddListener(OnButtonClick);
		//convo = new List<string>();
		dialogueNum = 0;
		convoNum = 0;
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
		if(convoNum > dialogueAmount - 1)
		{
			dialogueNum++;
			switchToMap = true;
		}
	}

	/*! \brief Getter / setter for dialogueField
	 */
	public Text DialogueField
	{
		get { return dialogueField; }
		set { dialogueField = value; }
	}

	public int DialogueNum
	{
		get { return dialogueNum; }
		set { dialogueNum = value; }
	}

	public int ConvoNum
	{
		get { return convoNum; }
	}

	public int DialogueAmount
	{
		set { dialogueAmount = value; }
	}

	/*! \brief Getter / setter for switchToMap
	 */
	public bool SwitchToMap
	{
		get { return switchToMap; }
		set { switchToMap = value; }
	}
}
