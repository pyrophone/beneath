using System.Collections;
using System.Collections.Generic;

using Mapbox.Utils;
using Mapbox.Unity.Map;

using UnityEngine;

[System.Serializable]
public class MarkerInfo
{
    public string markerName; //! The name of the marker
    public string markerPic; //! The filepath of the markerPic
	public Vector2d markerLoc; //! The location of the marker
    public int mRadius; //! The radius of the marker
	public int prereqMarker; //! The prereqMarker to this marker
}
