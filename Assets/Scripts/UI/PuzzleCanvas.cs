using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PuzzleCanvas : AbstractCanvas
{
    private Puzzle puzzle; //! the puzzle

    [SerializeField]
    private Text qText; //! question text
    [SerializeField]
    private Text aAText; //! answer 1 text
    [SerializeField]
    private Text aBText; //! answer 2 text
    [SerializeField]
    private Text aCText; //! answer 3 text

    [SerializeField]
    private Button aAButton; //! answer 1 button
    [SerializeField]
    private Button aBButton; //! answer 2 button
    [SerializeField]
    private Button aCButton; //! answer 3 button
    [SerializeField]
    private Button backButton; //! back button

    [SerializeField]
    private int amtWrong; //! amount times answered wrong

    /*! \brief Called when the object is initialized
	 */
    protected override void Awake()
    {
        base.Awake();

        //most of the buttons assigned thru inspector

        //assign back button listener 
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    protected override void Update()
    {
        
    }

    //public void

    /*! \brief Called when the correct answer is pressed
	 */
    private void OnRightAnswer()
    {
        int xp = (2 - amtWrong) * 50;
        if (xp == 0)
            xp = 2;
        Debug.Log("OnRightAnswer()");
        qText.text = puzzle.winText + "\n\nYou've Earned " + xp + "xp!";
        GameObject.Find("player").GetComponent<Player>().EXP += xp;
        aAButton.interactable = false;
        aBButton.interactable = false;
        aCButton.interactable = false;
    }

    /*! \brief next three mothods called when the specific button is pressed
	 */
    private void OnWrongAnswerA()
    {
        aAButton.interactable = false;
        OnWrongAnswer();
    }
    private void OnWrongAnswerB()
    {
        aBButton.interactable = false;
        OnWrongAnswer();
    }
    private void OnWrongAnswerC()
    {
        aCButton.interactable = false;
        OnWrongAnswer();
    }

    /*! \brief Called when the wrong answer is pressed
     */
    private void OnWrongAnswer()
    {
        amtWrong++;
        if (amtWrong >= 2)
        {
            amtWrong = 2;
            qText.text = puzzle.failText;
        }
        else
            qText.text = "Good guess, but that's not correct. Try again?";
    }

    /*! \brief Called when the back button is pressed
	 */
    private void OnBackButtonClick()
    {
        Debug.Log("OnBackButtonClick()");
        uiControl.SetCanvas(UIState.MAP);
    }

    /*! \brief Sets the puzzle displayed on the canvas, sets listeners for buttons
	 */
    public void SetPuzzle(Puzzle p)
    {
        puzzle = p;
        qText.text = p.puzzleQuestion;
        aAText.text = p.puzzleAnswers[0];
        aBText.text = p.puzzleAnswers[1];
        aCText.text = p.puzzleAnswers[2];

        aAButton.interactable = true;
        aBButton.interactable = true;
        aCButton.interactable = true;

        amtWrong = 0;

        //assign back button listener because I have no idea if the last listener actually gets set
        backButton.onClick.AddListener(OnBackButtonClick);

        // perhaps not the most efficient solution but it works?
        //assign listeners
        switch (p.correctAnswer)
        {
            case 0:
                aAButton.onClick.AddListener(OnRightAnswer);
                aBButton.onClick.AddListener(OnWrongAnswerB);
                aCButton.onClick.AddListener(OnWrongAnswerC);
                break;
            case 1:
                aAButton.onClick.AddListener(OnWrongAnswerA);
                aBButton.onClick.AddListener(OnRightAnswer);
                aCButton.onClick.AddListener(OnWrongAnswerC);
                break;
            case 2:
                aAButton.onClick.AddListener(OnWrongAnswerA);
                aBButton.onClick.AddListener(OnWrongAnswerB);
                aCButton.onClick.AddListener(OnRightAnswer);
                break;
        } 
    }
}
