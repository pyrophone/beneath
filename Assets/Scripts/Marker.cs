using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*! \class Marker
 *	\brief Acts as a marker on a map
 */
public class Marker : Mappable
{
    [SerializeField]
    protected bool isQuest; //! is the marker in a list of markers that make up a quest, and will need to be hidden until needed?
    [SerializeField]
    protected bool isPuzzle; //! does the marker include a puzzle?
    [SerializeField]
    protected bool triggered; //! is marker triggered?
    [SerializeField]
    protected string mName; //! name of the marker
    [SerializeField]
    protected Puzzle puzzle; //! puzzle type NOT IMPLEMENTED YET
    [SerializeField]
    protected string DialogueFile; //! directory of dialogue NOT IMPLEMENTED YET
    protected string dialogue;
    [SerializeField]
    protected GameObject player; //! reference to player object
    [SerializeField]
    protected int radius; //! radius of the marker (in meters) aka trigger distance

    private bool inRange; //! If the player is in range of the marker

    /*! \brief Called when the object is initialized
	 */
    private void Start()
	{
		this.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);

        //get player object
        player = GameObject.Find("player");
        dialogue = "This is test text. Have you heard of the tradegy of Darth Plageius the Wise? I thought not. It's not a story the Jedi would tell you. It's a Sith legend.";
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
		this.transform.localPosition = this.map.GeoToWorldPosition(this.loc);
        if (IsColliding())
            OnArrive();
	}

    /*! \brief Checks if the player object and the marker are colliding
	 *
	 * \return (bool) true if colliding, false if not.
	 */
    public bool IsColliding()
    {
        //code adapted from solution at: https://stackoverflow.com/questions/365826/calculate-distance-between-2-gps-coordinates
        #region distance
        int earthRadiusM = 6371000;

        float dLat = Mathf.Deg2Rad * (float)(loc.x - player.GetComponent<Player>().Loc.x);
        float dLon = Mathf.Deg2Rad * (float)(loc.y - player.GetComponent<Player>().Loc.y);

        float lat1 = Mathf.Deg2Rad * (float)(loc.x);
        float lat2 = Mathf.Deg2Rad * (float)(player.GetComponent<Player>().Loc.x);

        var a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2) * Mathf.Cos(lat1) * Mathf.Cos(lat2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a)); //optimize this part or limit frequency of calculation
        double distance = earthRadiusM * c;
        #endregion

        //DEBUG
        try
        {
            //DEBUG: show distance on main screen
            GameObject.Find("DistCounter").GetComponent<Text>().text = "Distance: " + distance.ToString("N2");
        }
        catch { }


        if (distance < radius)
            return true;

        return false;
    }

    /*! \brief The behavior triggered when player reaches the marker.
     * This will pretty much always include the puzzle/dialogue, as well
     * as preparing the next marker.
	 */
    public void OnArrive()
    {
        if(!inRange)
        {
			Handheld.Vibrate();
			inRange = true;
        }
    }

    public void OnMouseDown()
    {
		if(inRange && !EventSystem.current.IsPointerOverGameObject())
			triggered = true; // ideally triggered should not be set true until player has completed all events at marker
    }

    /*! \brief Gets the bool for if current marker is part of a quest
	 *
	 * \return (bool) The bool for isQuest
	 */
    public bool IsQuest
    {
        get { return isQuest; }
    }

    /*! \brief Gets the bool for if current marker has a puzzle
	 *
	 * \return (bool) The bool for isPuzzle
	 */
    public bool IsPuzzle
    {
        get { return isPuzzle; }
    }

    /*! \brief Gets the bool for if current marker is triggered
	 *
	 * \return (bool) The bool for isTrigger
	 */
    public bool Triggered
    {
        get { return triggered; }
        set { triggered = value; }
    }

    /*! \brief The Marker's real Name
	 *
	 * \return (string) the name of the marker
	 */
    public string MName
    {
        get { return mName; }
    }

    /*! \brief The Marker's radius in meters
    *
    * \return (int) the radius of the marker in meters
    */
    public int Radius
    {
        set { radius = value; }
        get { return radius; }
    }

    public string Dialogue
	{
		get { return dialogue; }
		set { dialogue = value; }
	}
}
