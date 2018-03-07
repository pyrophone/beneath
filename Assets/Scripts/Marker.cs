using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

using UnityEngine;

/*! \class Marker
 *	\brief Acts as a marker on a map
 */
public class Marker : Mappable
{
	[SerializeField]
	private GameObject pinObj; //! The GameObject of the pin

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		this.loc = new Vector2d(42.641787, 18.106856);
		this.instance = (GameObject)Instantiate(this.pinObj);
		this.instance.transform.localPosition = this.map.GeoToWorldPosition(this.loc);
		this.instance.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
		this.instance.transform.localPosition = this.map.GeoToWorldPosition(this.loc);
		this.instance.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
	}
}
