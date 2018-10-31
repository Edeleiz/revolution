using System;
using System.Collections.Generic;
using Code.Game.Map.Base;
using Code.Game.Map.Data;
using Code.Game.Map.View;
using UnityEngine;

namespace Code.Game.Map.Controller
{
    public class MapController : BaseController<MapData>
    {
        public Action<Color> OnRegionClicked;
        
        private MapView _mapView;
        public MapView MapView
        {
            get { return _mapView; }
            set
            {
                if (_mapView == value)
                    return;
                _mapView = value;
                SetMapView();
            }
        }

        private List<RegionController> _regionControllers;

        public MapController() : base(null)
        {
            
        }

        protected override void CommitData()
        {
            _regionControllers = new List<RegionController>();
            if (Data.Regions != null)
            {
                foreach (var regionData in Data.Regions)
                {
                    OnRegionDataAdded(regionData);
                }
            }
        }

        protected override void Clear()
        {
            OnRegionClicked = null;
            Data.OnRegionDataAdded = null;
        }

        protected override void AddDataListeners()
        {
            Data.OnRegionDataAdded += OnRegionDataAdded;
        }

        protected override void RemoveDataListeners()
        {
            Data.OnRegionDataAdded -= OnRegionDataAdded;
        }

        private void OnRegionDataAdded(RegionData data)
        {
            var controller = new RegionController(this) {Data = data};
            _regionControllers.Add(controller);
        }

        public void Initialize()
        {
            _regionControllers = new List<RegionController>();
        }

        public void Update()
        {
        
        }

        public void MakeAction()
        {
            var cam = Camera.main;
            System.Diagnostics.Debug.Assert(cam != null, "cam != null");
        
            RaycastHit hitInfo;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hitInfo))
                return;
        
            var spriteRenderer = _mapView.Renderer;
            var sprite = spriteRenderer.sprite;
        
            if (sprite.packed && sprite.packingMode == SpritePackingMode.Tight)
            {
                // Cannot use textureRect on tightly packed sprites
                Debug.LogError("SpritePackingMode.Tight atlas packing is not supported!");
                // TODO: support tightly packed sprites
                return;
            }
        
            // Convert world position to sprite position
            // worldToLocalMatrix.MultiplyPoint3x4 returns a value from based on the texWure dimensions (+/- half texDimension / pixelsPerUnit) )
            // 0, 0 corresponds to the center of the TEXTURE ITSELF, not the center of the trimmed sprite textureRect
            var spritePos = spriteRenderer.worldToLocalMatrix.MultiplyPoint3x4(ray.origin + (ray.direction * hitInfo.distance));
            var color = _mapView.GetRegionColor(spritePos);
            
            OnRegionClicked?.Invoke(color);
        }

        private void SetMapView()
        {
        
        }
    }
}