using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class MagnifierTracking : MonoBehaviour
	{
		public void OnMarkerFound(ARMarker marker){
			//Debug.Log("MARKER FOUND! WHEEEE!");
			MagnifierLensTexture.Visible = true;
		}

		public void OnMarkerLost(ARMarker marker){
			//Debug.Log("MARKER LOST! WHEEEE!");
			MagnifierLensTexture.Visible = false;
		}

		public void OnMarkerTracked(ARMarker marker)
		{
			MagnifierLensTexture.Pose = GetComponent<ARTrackedObject> ().transform.position;
			MagnifierLensTexture.Rotation = marker.transform.rotation;

			//MagnifierLensTexture.Pose = marker.transform.position;
			//Debug.Log ("marker tracked: " + marker.Tag);
		}
	}
}

