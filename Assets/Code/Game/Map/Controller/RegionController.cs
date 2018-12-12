using System.Collections.Generic;
using Code.Game.Map.Base;
using Code.Game.Map.Data;
using Code.Game.Map.View;
using UnityEngine;
using Zenject;

namespace Code.Game.Map.Controller
{
    public class RegionController : BaseController<RegionData>
    {
        private MapViewEvents _mapViewEvents = MapViewEvents.Instance;

        private RegionView _view;

        private bool _canChangeState = true;
        
        private List<RegionObjectController> _objectControllers;
        
        public RegionController()
        {
            
        }

        public void Initialize()
        {
            AddMapListeners();
        }

        protected override void CommitData()
        {
            _objectControllers = new List<RegionObjectController>();
            if (Data.Objects != null)
            {
                foreach (var regionData in Data.Objects)
                {
                    OnRegionObjectDataAdded(regionData);
                }
            }
        }

        public void SetView(RegionView view)
        {
            _view = view;

            foreach (var controller in _objectControllers)
            {
                _view.AddObjectView(controller.Data);
            }
        }

        public void UpdateView(Texture2D texture)
        {
            _view.Texture = texture;
        }

        protected override void AddDataListeners()
        {
            Data.OnRegionObjectDataAdded += OnRegionObjectDataAdded;
        }

        protected override void RemoveDataListeners()
        {
            Data.OnRegionObjectDataAdded -= OnRegionObjectDataAdded;
        }

        private void AddMapListeners()
        {
            _mapViewEvents.OnRegionClicked += OnRegionClicked;
            _mapViewEvents.OnRegionHovered += OnRegionHovered;
        }

        private void OnRegionObjectDataAdded(RegionObjectData data)
        {
            if (_view != null)
                _view.AddObjectView(data);
            
            var controller = new RegionObjectController() {Data = data};
//            controller.SetView(regionView);
//            controller.UpdateView(GetRegionTexture(data.Color));
            controller.Initialize();
            _objectControllers.Add(controller);
        }

        private void OnRegionHovered(Color color)
        {
            if (!_canChangeState)
                return;
            _view.IsHovered = color == Data.Color;
        }

        private void OnRegionClicked(Color color)
        {
            if (color == Data.Color)
            {
                _view.IsHovered = false;
                _view.IsSelected = true;
            }
            else if (color == Color.black)
            {
                _view.IsHovered = false;
                _view.IsSelected = false;
                _canChangeState = true;
            }
            else
            {
                _view.IsHovered = false;
                _view.IsSelected = false;
                _canChangeState = false;
            }
        }
    }
}