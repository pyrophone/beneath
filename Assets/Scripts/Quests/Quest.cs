using System.Collections;
using System.Collections.Generic;

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
	public List<MarkerInfo> markerGenList; //! The list of marker locations for the quest, used for generation
	public bool isTutorial; //! If the quest is the tutorial quest
	public int timeToCompleteMin; //! The minimum time to complete the quest
	public string reward; //! The quest reward
	public int prereqQuestID; //! The prerequisite quest ID, if 0, ignored
	public bool completed; //! If the quest is completed
	public List<int> dialogueNum; //! The amount of dialogue for each section
	public List<ConvoData> convo; //! All the dialogue said by the character
    public string filePath; //! Used for saving data
    public string qDescription; //! The quest description

    /*! \brief Gets quests from JSON
     *
     * \param (string) json - The string to get the JSON data
     *
     * \return (Quest) The quest extracted from the JSON
     */
    public static Quest GetFromJson(string json)
    {
		return JsonUtility.FromJson<Quest>(json);
    }

    /*! \brief Saves quests to JSON
     *
     * \param (Quest) data - The quest data to save to JSON
     *
     * \return (string) The string of the saved JSON data
     */
    public static string SaveToJson(Quest data)
    {
		return JsonUtility.ToJson(data, true);
    }
}
