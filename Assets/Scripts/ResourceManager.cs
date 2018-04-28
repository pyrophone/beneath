using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    static Dictionary<string, Sprite> sprites; //! Dictionary of all loaded sprites

	// Use this for initialization
	private void Start()
	{
		sprites = new Dictionary<string, Sprite>();

		Object[] bgs = Resources.LoadAll("Backgrounds", typeof(Sprite));
		Object[] chars = Resources.LoadAll("Characters", typeof(Sprite));
		Object[] achieve = Resources.LoadAll("AchievementItems", typeof(Sprite));

		foreach(Sprite s in bgs)
		{
			sprites.Add("Backgrounds/" + s.name, s);
		}

		foreach(Sprite s in chars)
		{
			sprites.Add("Characters/" + s.name, s);
		}

		foreach(Sprite s in achieve)
		{
			sprites.Add("AchievementItems/" + s.name, s);
		}
	}

	// Update is called once per frame
	private void Update()
	{

	}

	/*! \brief Gets the specified sprite
	 *
	 * \param (string) spriteName - The name of the sprite to find
	 *
	 * \return (Sprite) The sprite (null is non are found)
	 */
	static public Sprite GetSprite(string spriteName)
	{
		Sprite s;

		try
		{
			s = sprites[spriteName];
		}

		catch
		{
			s = null;
		}

		return s;
	}
}
