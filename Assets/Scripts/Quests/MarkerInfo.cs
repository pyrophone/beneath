using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;

using UnityEngine;

[System.Serializable]
public class MarkerInfo
{
	public Vector2d markerLoc; //! The location of the marker
	public int prereqMarker; //! The prereqMarker to this marker
	public string markerName; //! The real name of the marker
}
