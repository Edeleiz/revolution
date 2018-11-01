using Code.Game.Map.Base;
using Code.Game.Map.Data;
using UnityEngine;
using Zenject;

namespace Code.Game.Map.Controller
{
    public class RegionController : BaseController<RegionData>
    {
        [Inject] 
        private MapViewEvents _mapViewEvents;
        
        public RegionController()
        {
            
        }

        public override void Initialize()
        {
            AddMapListeners();
        }

        private void AddMapListeners()
        {
            _mapViewEvents.OnRegionClicked += OnRegionClicked;
        }

        private void OnRegionClicked(Color color)
        {
            if (color == Data.Color)
            {
                Debug.Log(Data.Name);
            }
        }
    }
}