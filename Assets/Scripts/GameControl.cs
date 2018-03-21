using System.Collections;
using System.Collections.Generic;

using Mapbox.Unity.Map;

using UnityEngine;
using UnityEngine.SceneManagement;

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

    /*! \brief Called when the game is initialized (ensures this code runs first no matter what)
	 */
    private void Awake()
    {
        DontDestroyOnLoad(this);
        player = (GameObject)Instantiate(playerPrefab);
        player.GetComponent<Player>().Map = this.map;
        player.name = "player";
        DontDestroyOnLoad(player);
    }

    /*! \brief Called when the object is initialized
	 */
    private void Start()
	{
		//TODO: something that needs to go here everytime the scene is loaded
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
        //for now, this will test vuforia by switching the scene on click or tap.
        if (Input.GetMouseButtonDown(0) && Input.touchCount > 2)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(1);
            else
                SceneManager.LoadScene(0);
        }

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
