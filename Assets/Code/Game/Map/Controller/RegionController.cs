using Code.Game.Map.Base;
using Code.Game.Map.Data;
using UnityEngine;

namespace Code.Game.Map.Controller
{
    public class RegionController : BaseController<RegionData>
    {
        public RegionController(MapController map) : base(map)
        {
            AddMapListeners();
        }

        private void AddMapListeners()
        {
            _mapController.OnRegionClicked += OnRegionClicked;
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