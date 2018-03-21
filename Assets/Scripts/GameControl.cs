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

    /*! \brief Called when the scene is initialized (ensures this code runs first no matter what)
	 */
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /*! \brief Called when the object is initialized
	 */
    private void Start()
	{

        //if PlayerObject exists, use that, if not, make another one
        if ((player = GameObject.Find("player")) is GameObject) { }
        else
        {
            player = Instantiate(playerPrefab);
            player.GetComponent<Player>().Map = this.map;
            player.name = "player";
            DontDestroyOnLoad(player);
        }
        
    }

	/*! \brief Updates the object
	 */
	private void Update()
	{
        //for now, this will test vuforia by switching the scene on click or tap.
        if (Input.GetMouseButtonDown(0) && (Input.touchCount > 2 || Application.platform == RuntimePlatform.WindowsEditor))
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
