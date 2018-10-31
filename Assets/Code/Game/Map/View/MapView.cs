using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Game.Map.View
{
	public class MapView : MonoBehaviour
	{
		[SerializeField]
		private Texture2D _mapTexture;
		public Texture2D MapTexture
		{
			get { return _mapTexture; }
			private set { _mapTexture = value; }
		}


		[SerializeField]
		private Texture2D _regionTexture;
		public Texture2D RegionTexture
		{
			get { return _regionTexture; }
			private set { _regionTexture = value; }
		}
	
		[SerializeField]
		private SpriteRenderer _renderer;
		public SpriteRenderer Renderer
		{
			get { return _renderer; }
			private set { _renderer = value; }
		}

		private List<Color> _regionColors;
	
		// Use this for initialization
		void Awake() 
		{
			Initialize();
			//Debug
			SetColors();
			print(_regionColors.Count);
		}

		// Update is called once per frame
		void Update() 
		{
		
		}

		private void Initialize()
		{
			Assert.IsNotNull(_renderer);

			_renderer.sprite = Sprite.Create(_mapTexture, new Rect(0.0f, 0.0f, _mapTexture.width, _mapTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
			_regionColors = new List<Color>();
		}

		private void SetColors()
		{
			foreach (var pixel in _regionTexture.GetPixels())
			{
				if (!_regionColors.Any(c => c.CompareRGB(pixel)))
				{
					_regionColors.Add(pixel);
				}
			}
		}

		public Color GetRegionColor(Vector3 texPosition)
		{
			Rect textureRect = _renderer.sprite.textureRect;
			float pixelsPerUnit = _renderer.sprite.pixelsPerUnit;
			float halfRealTexWidth = _mapTexture.width * 0.5f; // use the real texture width here because center is based on this -- probably won't work right for atlases
			float halfRealTexHeight = _mapTexture.height * 0.5f;
			// Convert to pixel position, offsetting so 0,0 is in lower left instead of center
			int texPosX = (int)(texPosition.x * pixelsPerUnit + halfRealTexWidth);
			int texPosY = (int)(texPosition.y * pixelsPerUnit + halfRealTexHeight);
			// Check if pixel is within texture
			if (texPosX < 0 || texPosX < textureRect.x || texPosX >= Mathf.FloorToInt(textureRect.xMax)) return default(Color); // out of bounds
			if (texPosY < 0 || texPosY < textureRect.y || texPosY >= Mathf.FloorToInt(textureRect.yMax)) return default(Color); // out of bounds
			// Get pixel color
			var color = _regionTexture.GetPixel(texPosX, texPosY);
			return color;
		}
	}
}
