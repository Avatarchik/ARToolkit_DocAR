using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RaycastTest : MonoBehaviour {
	
	public GameObject goTo, goFrom;

	//public GameObject body;
	//public GameObject magnifier;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 rayDirection = new ((MainMarker.localPosition - lensMarker.localPosition));
		//RaycastHit hit;
		RaycastHit[] hits;
		//bool icollision = false;
		//collision = Physics.Raycast(lensMarker.localPosition, rayDirection, out hit);

		Ray ray = new Ray(goFrom.transform.position, 
						  goTo.transform.position - goFrom.transform.position);
		
		hits = Physics.RaycastAll( ray );

		if (hits.Length > 0) 
		{
			RaycastHit[] slicesHit = hits
				.ToList()
				.Where ((h => h.collider.name.Contains ("mri_slice_")))
				.ToArray();

			int count = slicesHit.Length;

			//Debug.Log (hits.Length);
		}

		//
//		if (collision == false) {
//			Debug.Log ("null");
//		} else {
//			Debug.Log (hit.point);
//		}
	}
}
