using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code.Game.Map.View
{
    public class RegionView : MonoBehaviour
    {
        private Texture2D _texture;
        public Texture2D Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                if (_texture != null)
                    UpdateTexture();
            }
        }
        
        private bool _isHovered;
        public bool IsHovered
        {
            get { return _isHovered; }
            set
            {
                _isHovered = value;
                UpdateState();
            }
        }
        
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                UpdateState();
            }
        }

        [SerializeField]
        private SpriteRenderer _renderer;
        public SpriteRenderer Renderer
        {
            get { return _renderer; }
            private set { _renderer = value; }
        }

        [SerializeField] private RegionObjectView _objectPrefab;
        
        private List<RegionObjectView> _objectViews;
        
        // Use this for initialization
        void Awake() 
        {
            Initialize();
        }

        void Update()
        {

        }

        private void Initialize()
        {
            _objectViews = new List<RegionObjectView>();
        }

        public RegionObjectView AddObjectView(RegionObjectData data)
        {
            var objectView = GameObject.Instantiate(_objectPrefab, this.transform);
            objectView.transform.localPosition = data.MapPosition;
            _objectViews.Add(objectView);

            return objectView;
        }

        private void UpdateTexture()
        {
            _renderer.sprite = Sprite.Create(_texture, new Rect(0.0f, 0.0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            UpdateState();
        }

        private void UpdateState()
        {
            if (_isSelected)
            {
                _renderer.enabled = true;
                return;
            }
            
            _renderer.enabled = _isHovered;
        }

        private void OnDestroy()
        {
            Texture = null;
        }
    }
}