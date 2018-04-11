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

    /*! \brief Called when the object is initialized
	 */
    protected override void Awake()
    {
        base.Awake();

        qText = transform.Find("Q").GetComponent<Text>();
        aAText = transform.Find("A1").GetComponentInChildren<Text>();
        aBText = transform.Find("A2").GetComponentInChildren<Text>();
        aCText = transform.Find("A3").GetComponentInChildren<Text>();
        aAButton = transform.Find("A1").GetComponent<Button>();
        aBButton = transform.Find("A2").GetComponent<Button>();
        aCButton = transform.Find("A3").GetComponent<Button>();
        backButton = transform.Find("BackButton").GetComponent<Button>();

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
        Debug.Log("OnRightAnswer()");
        uiControl.SetCanvas(UIState.MAP);
    }

    /*! \brief Called when the wrong answer is pressed
	 */
    private void OnWrongAnswer()
    {
        switch (puzzle.correctAnswer)
        {
            case 0:
                aBButton.GetComponent<Image>().color = Color.red;
                aCButton.GetComponent<Image>().color = Color.red;
                break;
            case 1:
                aAButton.GetComponent<Image>().color = Color.red;
                aCButton.GetComponent<Image>().color = Color.red;
                break;
            case 2:
                aAButton.GetComponent<Image>().color = Color.red;
                aBButton.GetComponent<Image>().color = Color.red;
                break;
        }
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

        aAButton.GetComponent<Image>().color = Color.white;
        aAButton.GetComponent<Image>().color = Color.white;
        aAButton.GetComponent<Image>().color = Color.white;

        // perhaps not the most efficient solution but it works?
        //assign listeners
        switch (p.correctAnswer)
        {
            case 0:
                aAButton.onClick.AddListener(OnRightAnswer);
                aBButton.onClick.AddListener(OnWrongAnswer);
                aCButton.onClick.AddListener(OnWrongAnswer);
                break;
            case 1:
                aAButton.onClick.AddListener(OnWrongAnswer);
                aBButton.onClick.AddListener(OnRightAnswer);
                aCButton.onClick.AddListener(OnWrongAnswer);
                break;
            case 2:
                aAButton.onClick.AddListener(OnWrongAnswer);
                aBButton.onClick.AddListener(OnWrongAnswer);
                aCButton.onClick.AddListener(OnRightAnswer);
                break;
        } 
    }
}
