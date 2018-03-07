using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;

using UnityEngine;

/*! \class Quest
 *	\brief Holds information of quests
 */
public class Quest : MonoBehaviour
{
	[SerializeField]
	private GameObject marker; //! Marker GameObject

	private int id; //! The ID of the quest
	private string name; //! The name of the quest
	private List<GameObject> markerList; //! The list of markers for the quest
	private bool isTutorial; //! If the quest is the tutorial quest
	private int timeToCompleteMin; //! The minimum time to complete the quest
	private string reward; //! The quest reward
	private int prereqQuestID; //! The prerequisite quest ID

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		markerList = new List<GameObject>();

		//NOTE: This code is temporary for testing quests, just creates a bunch of markers and sets them
		GameObject m = (GameObject)Instantiate(marker);
		m.GetComponent<Marker>().Loc = new Vector2d(42.641787, 18.106856);
		m.GetComponent<Marker>().Map = GetComponent<GameControl>().Map;
		markerList.Add(m);

		m = (GameObject)Instantiate(marker);
		m.GetComponent<Marker>().Loc = new Vector2d(42.641787, 18.107329);
		m.GetComponent<Marker>().Map = GetComponent<GameControl>().Map;
		markerList.Add(m);

		m = (GameObject)Instantiate(marker);
		m.GetComponent<Marker>().Loc = new Vector2d(42.641361, 18.108965);
		m.GetComponent<Marker>().Map = GetComponent<GameControl>().Map;
		markerList.Add(m);

		m = (GameObject)Instantiate(marker);
		m.GetComponent<Marker>().Loc = new Vector2d(42.640865, 18.110373);
		m.GetComponent<Marker>().Map = GetComponent<GameControl>().Map;
		markerList.Add(m);
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{

	}

	/*! \brief Loads the markers
	 *
	 * \param (string) fileDirectory - The directory to load the quest
	 * \param (int) questID - The ID of the quest
	 */
	public void LoadMarkers(string fileDirectory, int questID)
	{

	}

	/*! \brief Initializes the quest
	 */
	public void StartQuest()
	{

	}

	/*! \brief Runs upon quest completion
	 */
	public void OnComplete()
	{

	}

	/*! \brief Gets the id
	 *
	 * \return (int) The id of the quest
	 */
	public int ID
	{
		get { return this.id; }
	}

	/*! \brief Gets the name
	 *
	 * \return (string) The name of the quest
	 */
	public string Name
	{
		get { return this.name; }
	}

	/*! \brief Gets the marker list
	 *
	 * \return (List<Marker>) The marker list of the quest
	 */
	public List<GameObject> MarkerList
	{
		get { return this.markerList; }
	}

	/*! \brief Gets the minimum time to complete
	 *
	 * \return (int) The minimum of the quest
	 */
	public int TimeToCompleteMin
	{
		get { return this.timeToCompleteMin; }
	}
}
