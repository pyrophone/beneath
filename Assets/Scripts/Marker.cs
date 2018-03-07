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
	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		this.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
		this.transform.localPosition = this.map.GeoToWorldPosition(this.loc);
		//this.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
	}
}
