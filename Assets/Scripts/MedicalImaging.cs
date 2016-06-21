using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System;


public class MedicalImaging
{
	public static Texture2D texture;
	public static Texture texAlphaMap;
	public static int slices;

	public static void LoadMRISlices(Texture2D texture, Texture alphamap)
	{
		MedicalImaging.texture = texture;
		texAlphaMap = alphamap;
		slices = 97;

		Debug.Log ("--"+texture.name);
		Debug.Log ("#sprites: "+slices.ToString());
	}

	public static void BuildMRIModel(GameObject parent, int num_slices, 
		Vector3 origin, float step, Vector4 threshold)
	{
		origin = parent.transform.position;

		for (int slice_index = 0; slice_index < num_slices; slice_index++) 
		{
			GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);


			quad.name = "mri_slice_" + slice_index.ToString();

			// Transform the position of the current quad relative to the parent transformation hierarchy
			quad.transform.position = new Vector3(origin.x, origin.y, origin.z + slice_index + step);



			//applyMaterial(quad, "OpacityMask_Material");

			//try{
				
				// Apply 2D texture map to quad using spritesheet
				applyQuadTexture2D (quad, texture, slice_index);

				// Apply color filter threshold:
				var m_renderer  = quad.GetComponent< MeshRenderer>();
				//m_renderer.material.SetVector ("threshold", threshold); 

				//applyAlphaMap(quad, texAlphaMap);

				// Apply the Opacity Map Shader to the quad/slice 
				//  this should make the MRI object appear transparent and 3 dimensional
				//  instead of just a black opaque cube
				applyQuadShader (quad, "Custom/MRI_Image_OpacityMap");

				quad.transform.parent = parent.transform;
			//}
			//catch{}



		}


	}

	public static Texture2D cropTexture2D(Texture2D texture, Rect crop_rect)
	{
		// DIRTY but it works
		Rect textureRect = new Rect(0,0,texture.width, texture.height);
		textureRect = crop_rect;
		Texture2D croppedTexture = new Texture2D( (int)crop_rect.width, (int)crop_rect.height );
		Color[] pixels = texture.GetPixels(  (int)textureRect.x, 
			(int)textureRect.y, 
			(int)textureRect.width, 
			(int)textureRect.height );

		croppedTexture.SetPixels( pixels );
		croppedTexture.Apply();

		return croppedTexture;
	}

//	public static void applyMaterial(GameObject quad, String material_name)
//	{
//		MeshRenderer m_renderer;
//		Material m_material;
//
//		m_renderer  = quad.GetComponent< MeshRenderer>();
//
//		m_material = Resources.Load(material_name, typeof(Material)) as Material;
//
//
//		m_renderer.material = m_material;
//	}

	public static void applyAlphaMap(GameObject quad, Texture alphamap)
	{
		MeshRenderer m_renderer;

		m_renderer  = quad.GetComponent< MeshRenderer>();

		m_renderer.material.SetTexture("_AlphaMap", alphamap);
	}

	public static void applyQuadShader(GameObject quad, String shader_path)
	{
		MeshRenderer m_renderer;
		Shader shader;

		m_renderer  = quad.GetComponent< MeshRenderer>();

		shader = Shader.Find (shader_path);

		m_renderer.material.SetTexture("_MainTex", m_renderer.material.mainTexture);
		//m_renderer.material.SetTexture("_AlphaMap", m_renderer.material.mainTexture);
		m_renderer.material.shader = shader;
	}

	public static void applyQuadTexture2D(GameObject q, Texture2D texture, int slice)
	{
		MeshRenderer        m_renderer;
		Mesh                m_mesh;
		Material            m_material;

		m_renderer  = q.GetComponent< MeshRenderer >();
		m_mesh      = q.GetComponent< MeshFilter >().mesh;
		m_material  = m_renderer.material;

		int _uvTieX = 10;
		int _uvTieY = 10;
		Vector2 _size = new Vector2 (1.0f / _uvTieX , 1.0f / _uvTieY);

		// split into horizontal and vertical index
		int uIndex = slice % 10;
		int vIndex = slice / 10;


		// build offset
		// v coordinate is the bottom of the image in opengl so we need to invert.
		Vector2 offset = new Vector2 (uIndex * _size.x, 
			1.0f - _size.y - vIndex * _size.y);

		Texture2D subtex = MedicalImaging.cropTexture2D (texture, 
			new Rect(offset.x * texture.width,offset.y * texture.height,
			200, 180));


		m_material.SetTexture("_MainTex", subtex);
	}
}


