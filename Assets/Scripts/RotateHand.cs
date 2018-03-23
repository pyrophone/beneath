using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \class RotateHand
 *	\brief rotates object towards another one
 *	*Originally code from a simple Unity exercise*
 */
public class RotateHand : MonoBehaviour {

    [SerializeField]
    private bool isCompass; //! is this the compass?

    [SerializeField]
    private QControl quests; //! quests to get markers to point at from
    private GameObject target; //! the target to point to
    private GameObject player; //! reference to player object

    /*! \brief Called when the object is initialized
	 */
    private void Start()
    {
        if (!isCompass)
        {
            player = GameObject.Find("player");
        }
    }

    /*! \brief Updates the object
	 */
    private void Update() {
        CheckMarkers();
        if (isCompass)
            RotateToObjectCompass();
        else
            RotateToObject();
	}

    /*! \brief Checks for changed marker
	 */
    private void CheckMarkers()
    {
        string markerName = "q" + quests.CurQuest.id + ".marker" + quests.MarkerCurrent;
        target = GameObject.Find(markerName);
    }

    /*! \brief points object to another object
	 */
    private void RotateToObjectCompass()
    {
        //get angle from world marker pos and then apply rotation based on that angle
        Vector3 mPos = target.transform.position;
        Debug.Log("Mpos: " + mPos);
        //marker is roughly at this position lol
        float angle = Mathf.Atan2(mPos.z + 48, mPos.x + 25) * Mathf.Rad2Deg + 180;
        Debug.Log("Mangle: " + angle);
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    private void RotateToObject()
    {
        //get player's position
        Vector3 pPos = player.transform.position;
        Debug.Log("Ppos: " + pPos);

        //get angle from world marker pos and then apply rotation based on that angle
        Vector3 mPos = target.transform.position;       
        float angle = Mathf.Atan2(mPos.z - pPos.z, mPos.x - pPos.x) * Mathf.Rad2Deg - 120;
        Debug.Log("Pangle: " + angle);
        transform.rotation = Quaternion.Euler(90, angle, 0);

    }

    public QControl Quests
    {
        get { return quests; }
        set { quests = value; }
    }
}
