using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

using UnityEngine;

/*! \class Mappable
 *	\brief Base class for objects on maps
 */
public class Mappable : MonoBehaviour
{
	[SerializeField]
	protected AbstractMap map; //! Reference to the map object
	protected Vector2d loc; //! Location for the object (longitude and latitude)
	protected GameObject instance; //! The instance for the GameObject to spawn
}
