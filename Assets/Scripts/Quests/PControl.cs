using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

/*! \class PControl
 *	\brief Manages quests
 */
public class PControl : MonoBehaviour
{
    [SerializeField]
    private GameObject marker; //! Marker prefab
    private UIControl uiControl; //! The UI control component
    private List<Puzzle> puzzles; //! list of puzzle Markers
    private Object[] textAssets; //! The list of text assets

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
        puzzles = new List<Puzzle>();

        textAssets = Resources.LoadAll("JSON/Puzzles", typeof(TextAsset));

        foreach (TextAsset txt in textAssets)
        {
            Puzzle p = Puzzle.GetFromJson(txt.ToString());
            puzzles.Add(p);
        }

        LoadPuzzles();
    }

    /*! \brief Updates the object
	 */
    void Update()
    {

    }

    /*! \brief Loads the puzzle markers into the map
	 */
    private void LoadPuzzles()
    {
        for (int i = 0; i < puzzles.Count; i++)
        {
            GameObject m = Instantiate(marker);
            m.GetComponent<Marker>().Loc = puzzles[i].location;
            m.GetComponent<Marker>().Puzzle = puzzles[i];
            m.GetComponent<Marker>().Map = GetComponent<GameControl>().Map;
            m.GetComponent<Marker>().Radius = 30;
            m.GetComponent<Marker>().IsPuzzle = true;
            m.name = "p.marker";
        }
    }
}
