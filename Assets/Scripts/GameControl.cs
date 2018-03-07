using System.Collections;
using System.Collections.Generic;

using Mapbox.Unity.Map;

using UnityEngine;

/*! \class GameControl
 *	\brief Acts as a general game manager
 */
public class GameControl : MonoBehaviour
{
	[SerializeField]
	private GameObject playerPrefab; //! The prefab for the player

	[SerializeField]
	private AbstractMap map; //! The map

	private GameObject player; //! The player object

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		player = (GameObject)Instantiate(playerPrefab);
		player.GetComponent<Player>().Map = this.map;
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Gets the map data
	 *
	 * \return (AbstractMap) The map data
	 */
	public AbstractMap Map
	{
		get { return this.map; }
	}
}
