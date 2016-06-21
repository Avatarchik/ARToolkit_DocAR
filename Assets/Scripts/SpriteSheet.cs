using UnityEngine;
using System.Collections;

public class SpriteSheet : MonoBehaviour 
{
	public int _uvTieX = 10;
	public int _uvTieY = 10;
	public int _fps = 10;

	public Texture2D texture;

	private Vector2 _size;
	private MeshRenderer _myRenderer;
	private int _lastIndex = -1;

	void Start () 
	{
		_size = new Vector2 (1.0f / _uvTieX , 1.0f / _uvTieY);
		_myRenderer  = GetComponent< MeshRenderer >();
		if(_myRenderer == null)
			enabled = false;

		var m_material  = _myRenderer.material;
		//m_material.mainTexture = texture;
	}
	// Update is called once per frame
	void Update()
	{
		this.transform.localScale = new Vector3(0.12f,0.12f,1.0f);

		// Calculate index
		_lastIndex++;
		int index = (int)(_lastIndex) % (_uvTieX * _uvTieY);
		if(index != _lastIndex)
		{
			// split into horizontal and vertical index
			int uIndex = index % _uvTieX;
			int vIndex = index / _uvTieY;

			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
			Vector2 offset = new Vector2 (uIndex * _size.x, 
				1.0f - _size.y - vIndex * _size.y);

			Texture2D subtex = MedicalImaging.cropTexture2D (texture, new Rect(offset.x,offset.y,
																200,180));

			//_myRenderer.material.SetTextureOffset ("_MainTex", offset);
			//_myRenderer.material.SetTextureScale ("_MainTex", _size);

			_lastIndex = index;

			_myRenderer.material.SetTexture ("_MainTex", subtex);
			MedicalImaging.applyQuadShader (this.gameObject, "Custom/MRI_Image_OpacityMap");
            //MedicalImaging.applyQuadShader(this.gameObject, "Custom/GLSL_basic_shader");
        }
	}


}