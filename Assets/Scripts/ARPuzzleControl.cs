using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ARPuzzleControl : MonoBehaviour
{
    [SerializeField]
    private int xpGain;

    [SerializeField]
    private Text congrats; // reference to the textbox on the vuforia canvas

    private Player player; //! The instantiated prefab of Player
    
    
    // Use this for initialization
    void Start ()
    {
        // congrats.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false); // deactivates the coin item when tapped
        player.EXP += xpGain; // increments and sets the player's exp variable to 200
        congrats.text = "Congratulations!";
    }
}
