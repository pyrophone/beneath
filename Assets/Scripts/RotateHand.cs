using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;

/*! \class RotateHand
 *	\brief rotates object towards another one
 *	*Originally code from a simple Unity exercise*
 */
public class RotateHand : MonoBehaviour {

    /// <summary>
    /// TODO: REPLACE ALL THIS STUFF TO GYROSCOPE REFERENCES AND MAKE THE COMPASS POINT TO REAL WORLD TARGETS
    /// </summary>

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
            if (!CheckGyro())
            {
                Destroy(gameObject);
            }
        }
    }

    /*! \brief Updates the object
	 */
    private void Update()
    {    
        if (isCompass)
        {
            //wrap in try catch b/c otherwise this will crash with no active quest
            try
            {
                CheckMarkers();
                RotateToObjectCompass();
            }
            catch (NullReferenceException)
            {
                gameObject.SetActive(false);
            }
        }    
        else
            RotateToObject();
	}

    /*! \brief Checks for changed marker
	 */
    private void CheckMarkers()
    {
        target = GameObject.Find(quests.NextMarkerString());

    }

    /*! \brief rotates compass to marker
	 */
    private void RotateToObjectCompass()
    {
        //get angle from world marker pos and then apply rotation based on that angle
        Vector3 mPos = target.transform.position;
        Debug.Log("Mpos: " + mPos);
        //marker is now at 0,0. so it's easy now
        float angle = Mathf.Atan2(mPos.z, mPos.x) * Mathf.Rad2Deg;
        Debug.Log("Mangle: " + angle);
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    /*! \brief rotates to player direction
	 */
    private void RotateToObject()
    {
        //check if gyro is active
        if (SystemInfo.supportsGyroscope)
        {
            //DEBUG
            //try
            //{
            //    //DEBUG: show location on main screen
            //    GameObject.Find("GeoAttitude").GetComponent<Text>().text = "gyro: " + Input.gyro.attitude;
            //}
            //catch { }

            //set arrow direction to gyro
            Quaternion gyro = Input.gyro.attitude;
            gyro.x = 0; gyro.y = 0;
            transform.rotation = gyro;

        }

    }

    /*! \brief checks if Gyro is supported
	 */
    private bool CheckGyro()
    {
        if (Input.gyro.enabled = SystemInfo.supportsGyroscope)
            return true;
        return false;
    }

    public QControl Quests
    {
        get { return quests; }
        set { quests = value; }
    }
}
