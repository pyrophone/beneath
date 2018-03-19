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
    private GameObject marker; //! marker prefab

    [SerializeField]
    private int id; //! The ID of the quest
    [SerializeField]
    private string name; //! The name of the quest
    [SerializeField]
    private List<Vector2d> markerGenList; //! The list of marker locations for the quest, used for generation
    private List<GameObject> markerList; //! holds the markers for the quest for easy access
    private int markerCurrent; //! current marker index in the list
    [SerializeField]
    private bool isTutorial; //! If the quest is the tutorial quest
    [SerializeField]
    private int timeToCompleteMin; //! The minimum time to complete the quest
    [SerializeField]
    private string reward; //! The quest reward
    [SerializeField]
    private int prereqQuestID; //! The prerequisite quest ID, if nil, ignored

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		markerList = new List<GameObject>();
        LoadMarkers("ReplaceMeWithRealThing", id);
        markerCurrent = 0;
        

	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
        ProgressQuest();
	}

	/*! \brief Loads the markers, sets all parameters
	 *
	 * \param (string) fileDirectory - The directory to load the quest
	 * \param (int) questID - The ID of the quest
	 */
	public void LoadMarkers(string fileDirectory, int questID)
	{
        //!for now, marker lists will be set manually until JSON / XML integration is implemented
        //loop through the given marker list after loading and prep each marker on the map.
        for (int i = 0; i < markerGenList.Count; i++)
        {
            GameObject m = Instantiate(marker);
            m.GetComponent<Marker>().Loc = markerGenList[i];
            m.GetComponent<Marker>().Map = GetComponent<GameControl>().Map;
            m.GetComponent<Marker>().Radius = 15;
            m.name = "q" + id + "." + "marker" + i;
            if (i != 0)
            {
                m.SetActive(false);
            }
            markerList.Add(m);
        }
    }

	/*! \brief progresses the quest, advances one marker
	 */
	public void ProgressQuest()
	{
        if (markerList[markerCurrent].GetComponent<Marker>().Triggered)
        {
            markerList[markerCurrent].SetActive(false);
            markerCurrent++;
            markerList[markerCurrent].SetActive(true);
        }

        if (markerCurrent == markerList.Count)
            OnComplete();

    }

	/*! \brief Runs upon quest completion, synonymous with reaching and completing all tasks at final marker
	 */
	public void OnComplete()
	{
        //unload markers
        for (int i = markerList.Count; i >= 0; i--)
        {
            Destroy(markerList[i]);
            markerList.RemoveAt(i);
        }
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
