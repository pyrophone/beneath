using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

/*! \class QControl
 *	\brief Manages quests
 */
public class QControl : MonoBehaviour
{
	[SerializeField]
    private GameObject marker; //! Marker prefab
    [SerializeField]
    private GameObject compass; //! Compass object
	[SerializeField]
	private Quest curQuest; //! The current quest
	private UIControl uiControl; //! The UI control component
    private bool questShouldFinish; //! If the quest should finish
	Dictionary<Quest, bool> quests; //! Dictionary of quests and their completion status
	private List<GameObject> markerList; //! The list of markers
	private Dictionary<int, List<int>> markerDict; //! Dictionary to tell which markers rely on which
    private int markerCurrent; //! Current marker index in the list
    private Object[] textAssets; //! The list of text assets
    private int questAssetNum; //! The quest number for the asset list

    /*! \brief Called on startup
     */
    private void Awake()
    {
		uiControl = GetComponent<UIControl>();
    }

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		markerList = new List<GameObject>();
		quests = new Dictionary<Quest, bool>();
		markerDict = new Dictionary<int, List<int>>();

		textAssets = Resources.LoadAll("JSON/Quests", typeof(TextAsset));

		foreach(TextAsset txt in textAssets)
		{
			Quest q = Quest.GetFromJson(txt.ToString());
			quests.Add(q, false);
		}

		SetCurrentQuest(null);
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
        //wrap in try catch because this thing screams at the dumbest things that don't matter
        try
        {
            if (!questShouldFinish && curQuest != null)
                ProgressQuest();
        }
        catch { }

	}

	/*! \brief Loads the markers into the map
	 */
	private void LoadMarkers()
	{
		if(curQuest != null)
		{
			for (int i = 0; i < curQuest.markerGenList.Count; i++)
			{
				GameObject m = Instantiate(marker);
				m.GetComponent<Marker>().Loc = curQuest.markerGenList[i].markerLoc;
				m.GetComponent<Marker>().Map = GetComponent<GameControl>().Map;
				m.GetComponent<Marker>().Radius = curQuest.markerGenList[i].mRadius;
                m.GetComponent<Marker>().MName = curQuest.markerGenList[i].markerName;
                m.GetComponent<Marker>().MPic = curQuest.markerGenList[i].markerPic;
                m.name = "q" + curQuest.id + "." + "marker" + i;

				if (i != 0)
				{
					m.SetActive(false);
				}

				markerList.Add(m);

				markerDict.Add(i, new List<int>());

				if(curQuest.markerGenList[i].prereqMarker > -1)
					markerDict[curQuest.markerGenList[i].prereqMarker].Add(i);
			}

			markerCurrent = 0;
		}
	}

	/*! Sets the current quest
	 *
	 * \param (Quest) q - The quest to set the current quest to
	 */
	public void SetCurrentQuest(Quest q)
	{
		ClearMarkers();
		curQuest = q;
		questShouldFinish = false;

		if(curQuest != null)
		{
			uiControl.QLCanvas.SetActiveQuestText(curQuest.name);
			LoadMarkers();

			//enough info to set up compass
			compass.SetActive(true);
		}

		else
		{
			uiControl.QLCanvas.SetActiveQuestText("No Active Quest");
		}

		uiControl.QLCanvas.RefreshQuestList(quests, curQuest);

		markerCurrent = 0;
	}

	/*! \brief progresses the quest, advances one marker
	 */
	public void ProgressQuest()
	{
		if(markerDict.Count != 0)
		{
			for(int i = 0; i < markerDict.Count; i++)
			{
				if(markerList[markerDict.ElementAt(i).Key].GetComponent<Marker>().Triggered)
				{
					markerCurrent = markerDict.ElementAt(i).Key;
					markerList[markerDict.ElementAt(i).Key].SetActive(false);

					if(curQuest.dialogueNum[markerCurrent] >= 0)
						uiControl.SetCanvas(UIState.DIALOGUE);

					markerList[markerDict.ElementAt(i).Key].GetComponent<Marker>().Triggered = false;

					if(markerDict.ElementAt(i).Value.Count == 0)
						OnComplete();

					else
					{
						foreach(var nextMarker in markerDict.ElementAt(i).Value)
							markerList[nextMarker].SetActive(true);
					}
				}
			}
		}
    }

    /*! \brief finds the next active marker in quest
	 */
    public string NextMarkerString()
    {
        GameObject marker = null;
        string mark = null;
        for (int i = 0; marker == null && i < 100; i++)
        {
            mark = "q" + curQuest.id + ".marker" + i;
            marker = GameObject.Find(mark);
        }
        return mark;
    }

    /*! \brief Clears the quest markers
     */
    public void ClearMarkers()
	{
		//Unload markers
		for (int i = markerList.Count - 1; i >= 0; i--)
		{
			Destroy(markerList[i]);
			markerList.RemoveAt(i);
		}
		markerDict.Clear();
	}

    /*! \brief Runs upon quest completion, synonymous with reaching and completing all tasks at final marker
	 */
	public void OnComplete()
	{
		//Mark that the quest is complete and write it to the quest file
		curQuest.completed = true;
		quests[curQuest] = true;
		questShouldFinish = true;
        GameObject.Find("player").GetComponent<Player>().EXP += curQuest.rewardXP;
        ClearMarkers();
	}

	/*! \brief Getter / Setter for the current quest
	 */
	public Quest CurQuest
	{
		get { return curQuest; }
		set { curQuest = value; }
	}

	/*! \brief Getter / Setter for the quest finish bool
	 */
	public bool QuestShouldFinish
	{
		get { return questShouldFinish; }
		set { questShouldFinish = value; }
	}

	/*! \brief Gettter for the quest dictionary
	 */
	 public Dictionary<Quest, bool> Quests
	 {
		get { return quests; }
	 }

	/*! \brief Getter / Setter for the marker list
	 */
	public List<GameObject> MarkerList
	{
		get { return markerList; }
	}

    /*! \brief Getter / Setter for the marker list
	 */
    public int MarkerCurrent
    {
        get { return markerCurrent; }
        set { MarkerCurrent = value; }
    }

    /*! \brief Gets the quest with a certain ID
	 *
	 * \param (int) id - The id of the quest to look for
	 *
	 * \return (Quest) The quest with the corresponding id
     */
    public Quest GetQuest(int id)
    {
		foreach(var quest in quests)
		{
			if(quest.Key.id == id)
				return quest.Key;
		}

		return null;
    }
}
