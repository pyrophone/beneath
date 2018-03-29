using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

using UnityEngine;
using UnityEngine.UI;

/*! \class Player
 *	\brief Handles updating of the player object
 */
public class Player : Mappable
{
	private int xp; //! The players experience

	/*! \brief Called when the object is initialized
	 */
	IEnumerator Start()
	{
		this.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);

        //Setup the location info
        if (!Input.location.isEnabledByUser)
        {
            loc = new Vector2d(42.641787, 18.106856); //debug for testing markers on laptop
            yield break;
        }

        //edit as needed for accuracy
		Input.location.Start(5, 5);

		int maxWait = 30;

		while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		if(maxWait == 0) {
			Debug.Log("Timeout");
            yield break;
		}

        //Instantiate the player
        if (Input.location.status != LocationServiceStatus.Failed)
		{
			loc = new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude);
		}      
    }

	/*! \brief Updates the object
	 */
	void Update()
	{
		if(Input.location.status != LocationServiceStatus.Failed && Input.location.isEnabledByUser)
		{
			this.loc = new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }

        //DEBUG
        //try
        //{
        //    //DEBUG: show location on main screen
        //    GameObject.Find("GeoCounter").GetComponent<Text>().text = "loc: " + loc;
        //}
        //catch { }
        

        this.transform.localPosition = this.map.GeoToWorldPosition(this.loc);
	}

}
