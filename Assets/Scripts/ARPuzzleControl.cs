using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ARPuzzleControl : MonoBehaviour
{
    /*
    public GameObject canvas;
    public Button bttn;
    */
    // Use this for initialization
    void Start ()
    {
        // canvas.SetActive(false); // makes sure that the canvas is deactivated when the camera is opened
        
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false); // deactivates the coin item when tapped

        // Destroy(gameObject); // destroys the coin for from the image target

        Debug.Log("200 xp");

        // canvas.SetActive(true); // reactivates the canvas

        /*
        Button btn = bttn.GetComponent<Button>();
        btn.onClick.AddListener(Clicked); // this doesn't work
        Debug.Log("event listener added");
        */
    }
    
    /*
    // this doesn't work
    private void Clicked()
    {
        Debug.Log("Closed dat shizz doh");
    }
    */
}
