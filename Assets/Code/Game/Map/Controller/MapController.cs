using System;
using System.Collections.Generic;
using System.Linq;
using Code.Game.Map.Base;
using Code.Game.Map.Data;
using Code.Game.Map.View;
using TMPro;
using UnityEngine;

namespace Code.Game.Map.Controller
{
    public class MapController : BaseController<MapData>
    {
        
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

        public MapController() : base()
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
            var regionView = _mapView.AddRegionView(data);
            
            var controller = new RegionController() {Data = data};
            controller.SetView(regionView);
            controller.UpdateView(GetRegionTexture(data.Color));
            controller.Initialize();
            _regionControllers.Add(controller);
        }

        private Texture2D GetRegionTexture(Color regionColor)
        {
            var regionTexture = _mapView.RegionTexture;
            int x = 0;
            int y = 0;

            int left = -1, right = -1, top = -1, bottom = -1;

            for (x = 0; x < regionTexture.width; x++)
            {
                for (y = 0; y < regionTexture.height; y++)
                {
                    var pixel = regionTexture.GetPixel(x, y);
                    if (pixel.Equals(regionColor))
                    {
                        if (top < 0 || top > y)
                            top = y;
                        if (bottom < 0 || bottom < y)
                            bottom = y;
                        
                        if (left < 0 || left > x)
                            left = x;
                        if (right < 0 || right < x)
                            right = x;
                    }
                }
            }
            
            var texture = new Texture2D(right - left, bottom - top);

            var filteredPixels = regionTexture.GetPixels(left, top, right - left, bottom - top);
            var len = filteredPixels.Length;
            for (var i = 0; i < len; i++)
            {
                var color = filteredPixels[i];
                if (!color.CompareRGB(regionColor))
                    filteredPixels[i] = new Color(0, 0, 0, 0);
            }
            
            texture.SetPixels(filteredPixels);
            texture.Apply();
            
            return texture;
        }

        public override void Initialize()
        {
            _regionControllers = new List<RegionController>();
        }

        public void Update()
        {
        
        }

        private void SetMapView()
        {
        
        }
    }
}