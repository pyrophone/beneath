using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ARPuzzleControl : MonoBehaviour
{
    [SerializeField]
    private int xpGain;

    private Player player; //! The instantiated prefab of Player

    /*
    private bool getSomeXP;

    

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
        player.EXP += 200; // increments and sets the player's exp variable to 200
    }
}
