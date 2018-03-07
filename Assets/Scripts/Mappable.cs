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

	/*! \brief Gets and sets the map data
	 *
	 * \param (AbstractMap) value - The map to set
	 *
	 * \return (AbstractMap) The map get
	 */
	public AbstractMap Map
	{
		get { return this.map; }
		set { this.map = value; }
	}

	/*! \brief Gets and sets the location vector
	 *
	 * \param (Vector2d) value - The location vector to set
	 *
	 * \return (Vector2d) The location vector to get
	 */
	public Vector2d Loc
	{
		get { return this.loc; }
		set { this.loc = value; }
	}
}
