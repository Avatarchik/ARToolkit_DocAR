/*
 The following Medical VR X3D Application converted to an AR Application
 http://examples.x3dom.org/volren/volrenOpacityTestTF_aorta.xhtml
 http://examples.x3dom.org/volren/volrenMPR_aorta.xhtml
 http://examples.x3dom.org/volren/aorta4096.png
 */

using UnityEngine;
using System.Collections;

public class MRIObject : MonoBehaviour 
{

	public Texture2D texture;
	public Texture2D alphamap;

	public static Quaternion Rotation { get; set;}
	public static Vector3 Pose { get; set; }
	public static bool Visible { get; set; }

	public Vector4 Threshold = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);

	// Use this for initialization
	void Start () 
	{
		Pose = Vector3.zero;

		MedicalImaging.LoadMRISlices (texture, alphamap);
		MedicalImaging.BuildMRIModel (this.gameObject, MedicalImaging.slices, 
			Vector3.zero, 1.0f, Threshold);

		this.transform.Translate (new Vector3 (0.1f, 0.15f, -0.10f));
	}

	// Update is called once per frame
	void Update () 
	{
		this.transform.localScale = new Vector3(0.25f,0.25f,0.001f);

		//float a;
		//Vector3 x;

		//MRIObject.Rotation = Quternion.zero;

		//Rotation.ToAngleAxis (out a, out x);

		//this.transform.Rotate(Rotation.eulerAngles );

		//this.transform.RotateAround(this.transform.position,Vector3.left, Rotation.w);

		//transform.rotation = Quaternion.Lerp (transform.rotation, MRIObject.Rotation, Time.deltaTime * 1);

		//transform.Rotate(0, a, 0, Space.World);

//		this.transform.RotateAround(this.transform.position, 
//			Vector3, a );
	}
}