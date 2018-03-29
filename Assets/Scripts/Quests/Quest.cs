using System.Collections;
using System.Collections.Generic;
//using System.IO;

using Mapbox.Utils;
using Mapbox.Unity.Map;

using UnityEngine;

/*! \class Quest
 *  \brief Holds data about quests to be read from Json files
 */
[System.Serializable]
public class Quest
{
	public int id; //! The ID of the quest
	public string name; //! The name of the quest
	public List<Vector2d> markerGenList; //! The list of marker locations for the quest, used for generation
	public bool isTutorial; //! If the quest is the tutorial quest
	public int timeToCompleteMin; //! The minimum time to complete the quest
	public string reward; //! The quest reward
	public int prereqQuestID; //! The prerequisite quest ID, if 0, ignored
	public bool completed; //! If the quest is completed
	public List<int> dialogueAmount; //! The amount of dialogue for each section
	public List<string> convo; //! All the dialogue said by the charager
    public string filePath; //! Used for saving data

    public static Quest GetFromJson(string json)
    {
		return JsonUtility.FromJson<Quest>(json);
    }

    public static string SaveToJson(Quest data)
    {
		return JsonUtility.ToJson(data, true);
    }
}
