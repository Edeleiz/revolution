using System;
using System.Collections.Generic;
using Code.Game.Map.Base;
using Code.Game.Map.Source;
using UnityEngine;

namespace Code.Game.Map.Data
{
    public class MapData : BaseData<MapSource>
    {
        public Action<RegionData> OnRegionDataAdded;
        
        private List<RegionData> _regions;

        public List<RegionData> Regions => _regions;

        protected override void UpdateSource()
        {
            ParsedSource = JsonUtility.FromJson<MapSource>(_source.ToString());
            _regions = new List<RegionData>();

            foreach (var regionSource in ParsedSource.regions)
            {
                AddRegionData(regionSource);
            }
        }

        private void AddRegionData(RegionSource source)
        {
            var data = new RegionData();
            data.SetParsedSource(source);
            _regions.Add(data);
            OnRegionDataAdded?.Invoke(data);
        }
    }
}