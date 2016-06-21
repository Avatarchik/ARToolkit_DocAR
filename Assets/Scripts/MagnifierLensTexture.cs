using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;

public class MagnifierLensTexture : MonoBehaviour {

	public int currentSlice = 0; // use raycasting to get the current depth of the marker from a point in the human body

	public Texture2D texture;
	public Texture2D alphamap;

	public static bool Visible { get; set;}

//	public void OnCollisionEnter(Collision col)
//	{
//		if (col.gameObject.name == "BoundsDown") 
//		{
//			Debug.Log ("Magnifier hit ground. Setting depth to zero.");
//
//			if (DepthChangedEventHandler != null) 
//			{
//				DepthChangedEventHandler (pose);
//			}
//		}
//	}

	public static Quaternion Rotation { get; set; }

	public static Vector3 Pose { 
		get
		{ 
			return pose; 
		}
		set
		{ 
			pose = value;


			if (pose.y > prev.y) 
			{
				if (DepthChangedEventHandler != null) 
				{
					DepthChangedEventHandler (pose);
				}
			}
			if (pose.y < prev.y) 
			{
				if (DepthChangedEventHandler != null) 
				{
					DepthChangedEventHandler (pose);
				}
			}
		}
	}

	public delegate void DepthChangedDelegate(Vector3 marker_pose);
	public static event DepthChangedDelegate DepthChangedEventHandler;

	/// <summary>
	/// tolerance
	/// </summary>
	private const float TOL = 0.000001f;
	private const float MAX_HEIGHT = 0.01f;

	private static Vector3 pose = Vector3.zero;
	private static Vector3 prev = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		pose = Vector3.zero;
		DepthChangedEventHandler += MagnifierLensTexture_DepthChangedEventHandler;

		MedicalImaging.LoadMRISlices (texture, alphamap);


		loadSlice (currentSlice);
	}

	float time_since_last_slice = -9.0f;

	void MagnifierLensTexture_DepthChangedEventHandler (Vector3 marker_pose)
	{
		//float th = 0.461f;
		pose = marker_pose;

		MRIObject.Rotation = Rotation;

		float delay = 0.5f;

		if (Time.time >= time_since_last_slice + delay) 
		{
			if (pose.y <= 0.5534107f && pose.y >= 0.0f) 
			{
				int steps = MedicalImaging.slices;
				float step_trigger_dist = 0.25f / (float)steps;
				float tol = 0.05f * step_trigger_dist; // 11% tolerance of step distance

				if (pose.y > step_trigger_dist - tol && pose.y < pose.y + step_trigger_dist + tol) {

					currentSlice = (currentSlice + 1) % MedicalImaging.slices;
					time_since_last_slice = Time.time + delay;


				}
				else if (pose.y < pose.y - step_trigger_dist - 2*tol) {

					currentSlice = (currentSlice - 1) % MedicalImaging.slices;

					time_since_last_slice = Time.time + delay;
				}

			}
		}


	
//		if (pose.y <= prev.y - th) 
//		{
//			currentSlice = (currentSlice - 1) % MedicalImaging.slices;
//
//		}

		loadSlice (currentSlice);
	}
		
	// Update is called once per frame
	void Update () 
	{

		this.transform.localScale = new Vector3(0.12f,0.12f,1.0f);

		if (Input.GetKeyDown (KeyCode.D)) 
		{
			currentSlice = (currentSlice - 1) % MedicalImaging.slices;
			if (currentSlice < 0)
				currentSlice = MedicalImaging.slices - 1;

			loadSlice (currentSlice);
		}
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			currentSlice = (currentSlice + 1) % MedicalImaging.slices;
			if (currentSlice == MedicalImaging.slices + 1)
				currentSlice = 0;

			loadSlice (currentSlice);
		}


	}

	public void loadSlice(int slice)
	{
		if (slice < 0)
			slice = 0;
		if (slice > MedicalImaging.slices)
			slice = MedicalImaging.slices-1;
		currentSlice = slice;

		MedicalImaging.applyQuadTexture2D (this.gameObject, MedicalImaging.texture, slice);

		MedicalImaging.applyQuadShader (this.gameObject, "Custom/LensMask_Shader");

		Debug.Log ("slice: "+slice.ToString());

		string txt = 
			string.Format ("Slice -({0})", currentSlice.ToString ());
		var g = GameObject.Find ("CurrSliceTextBox").GetComponent<TextMesh> ();
		g.text = txt;
		g.fontSize = 14;

	}
		
}
