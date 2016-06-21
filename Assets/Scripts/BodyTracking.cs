using System;
using UnityEngine;
using System.Collections;

public class BodyTracking : MonoBehaviour 
{

	public void OnMarkerFound(ARMarker marker){
		//Debug.Log("MARKER FOUND! WHEEEE!");
		MRIObject.Visible = true;
	}

	public void OnMarkerLost(ARMarker marker){
		//Debug.Log("MARKER LOST! WHEEEE!");
		MRIObject.Visible = false;
	}

	public void OnMarkerTracked(ARMarker marker)
	{
		MRIObject.Pose = marker.transform.position;
		//Debug.Log ("marker tracked: " + marker.Tag);
	}
}
