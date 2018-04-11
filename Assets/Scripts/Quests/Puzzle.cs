using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;

using UnityEngine;

/*! \class Quest
 *  \brief Holds data about puzzles to be read from Json files, initially limited to multiple choice puzzles
 */
[System.Serializable]
public class Puzzle
{
	public string puzzleType;
    public Vector2d location;
	public string puzzleQuestion;
    public List<string> puzzleAnswers;
    public int correctAnswer; //! the correct answer
    public string filePath; //! Used for saving data

    /*! \brief Gets puzzles from JSON
     *
     * \param (string) json - The string to get the JSON data
     *
     * \return (Puzzle) The puzzle extracted from the JSON
     */
    public static Puzzle GetFromJson(string json)
    {
        return JsonUtility.FromJson<Puzzle>(json);
    }

    /*! \brief Saves puzzles to JSON
     *
     * \param (Puzzle) data - The puzzle data to save to JSON
     *
     * \return (string) The string of the saved JSON data
     */
    public static string SaveToJson(Puzzle data)
    {
        return JsonUtility.ToJson(data, true);
    }
}
