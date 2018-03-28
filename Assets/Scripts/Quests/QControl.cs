﻿using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;


/*! \class QControl
 *	\brief Manages quests
 */
public class QControl : MonoBehaviour
{
	[SerializeField]
    private GameObject marker; //! marker prefab
	[SerializeField]
	private Quest curQuest; //! The current quest
	private UIControl uiControl; //! The UI control component
    private bool questShouldFinish; //! If the quest should finish
	Dictionary<Quest, bool> quests; //! Dictionary of quests and their completion status
	private List<GameObject> markerList; //! The list of markers
    private int markerCurrent; //! Current marker index in the list
    private Object[] textAssets; //! The list of text assets
    private int questAssetNum; //! The quest number for the asset list

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{
		uiControl = GetComponent<UIControl>();

		markerList = new List<GameObject>();
		quests = new Dictionary<Quest, bool>();

		bool firstQuest = true;

		textAssets = Resources.LoadAll("JSON", typeof(TextAsset));

		foreach(TextAsset txt in textAssets)
		{
			Quest q = Quest.GetFromJson(txt.ToString());
			quests.Add(q, q.completed);

			//This is temporary, since you can't choose quests
			if(firstQuest)
			{
				curQuest = q;
				firstQuest = false;
				LoadMarkers();
				questAssetNum = 0;
			}
		}
	}

	/*! \brief Updates the object
	 */
	private void Update()
	{
		if(!questShouldFinish && curQuest != null)
			ProgressQuest();
	}

	/*! \brief Loads the markers into the map
	 */
	private void LoadMarkers()
	{
		for (int i = 0; i < curQuest.markerGenList.Count; i++)
		{
			GameObject m = Instantiate(marker);
			m.GetComponent<Marker>().Loc = curQuest.markerGenList[i];
			m.GetComponent<Marker>().Map = GetComponent<GameControl>().Map;
			m.GetComponent<Marker>().Radius = 15;
			m.name = "q" + curQuest.id + "." + "marker" + i;
			if (i != 0)
			{
				m.SetActive(false);
			}
			markerList.Add(m);
		}

		markerCurrent = 0;

        //enough info to set up compass
	}

	/*! \brief progresses the quest, advances one marker
	 */
	public void ProgressQuest()
	{
		if (markerList[markerCurrent].GetComponent<Marker>().Triggered)
        {
			markerList[markerCurrent].SetActive(false);
			if(curQuest.dialogueAmount[uiControl.Dial.DialogueNum] == 0)
				uiControl.Dial.DialogueNum++;

			else
				uiControl.SetCanvas(UIState.DIALOGUE);

			markerCurrent++;

			if (markerCurrent >= markerList.Count)
				OnComplete();

			else
				markerList[markerCurrent].SetActive(true);
		}
    }

    /*! \brief Runs upon quest completion, synonymous with reaching and completing all tasks at final marker
	 */
	public void OnComplete()
	{
		//Mark that the quest is complete and write it to the quest file
		curQuest.completed = true;
		quests[curQuest] = true;
		questShouldFinish = true;

        //unload markers
        for (int i = markerList.Count - 1; i >= 0; i--)
        {
            Destroy(markerList[i]);
            markerList.RemoveAt(i);
        }
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
    }
}
