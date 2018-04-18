using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ARPuzzleControl : MonoBehaviour
{
    [SerializeField]
    private Text mapXP;

    [SerializeField]
    private Text playerXP;

    [SerializeField]
    private int xpGain;
    /*
    private bool getSomeXP;

    private Player player; //! The instantiated prefab of Player

    private UIControl uiCont;

    private Button playerButton; //! The next button for the player screen
    private Text expText;   //! The Text component related to the player's experience
    */
    // Use this for initialization
    void Start ()
    {
        /*
        playerButton = GameObject.Find("PlayerButton").GetComponent<Button>();

        // getSomeXP = false;
        // Debug.Log(getSomeXP);
        // uiCont = GameObject.Find("GameManager").GetComponent<UIControl>();
        //mapXP = GameObject.Find("MapPlayerXP")

        GameObject gObj = GameObject.Find("GameManager");
        GameControl gc = gObj.GetComponent<GameControl>();
        gObj = gc.playerPrefab;
        player = gObj.GetComponent<Player>();
        */
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false); // deactivates the coin item when tapped
    }
    /*
    public int GainXp()
    {

        Debug.Log("t");
        if (getSomeXP == true)
        {
            int finalXP = 0;
            
            finalXP = player.GetExp() + xpGain;

            return finalXP;
        }
        else
        {
            return player.GetExp();
        }
        
    }
    */
}
