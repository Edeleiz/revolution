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
            var controller = new RegionController() {Data = data};
            _regionControllers.Add(controller);
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