using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

using UnityEngine;

/*! \class Player
 *	\brief Handles updating of the player object
 */
public class Player : Mappable
{
	[SerializeField]
	private GameObject playObj;
	private int experience;

	/*! \brief Called when the object is initialized
	 */
	IEnumerator Start()
	{
		if(!Input.location.isEnabledByUser)
			yield break;

		Input.location.Start();
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

		if(Input.location.status != LocationServiceStatus.Failed)
		{
			this.loc = new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude);
		}

		this.instance = (GameObject)Instantiate(playObj);
		this.instance.transform.localPosition = this.map.GeoToWorldPosition(this.loc);
		this.instance.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
	}

	/*! \brief Updates the object
	 */
	void Update()
	{
		if(Input.location.status != LocationServiceStatus.Failed)
		{
			this.loc = new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude);
			this.instance.transform.localPosition = this.map.GeoToWorldPosition(this.loc);
		}
	}
}
