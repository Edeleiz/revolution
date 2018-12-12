using System;
using System.Collections.Generic;
using Code.Game.Map.Base;
using Code.Game.Map.Source;
using UnityEngine;

namespace Code.Game.Map.Data
{
    public class RegionData : BaseData<RegionSource>
    {
        public Color Color { get; private set; }
        public string Name { get; private set; }
        public Vector3 MapPosition { get; private set; }
        
        public Action<RegionObjectData> OnRegionObjectDataAdded;
        
        private List<RegionObjectData> _objects;

        public List<RegionObjectData> Objects => _objects;

        protected override void UpdateSource()
        {
            Name = ParsedSource.name;
            Color parsedColor;
            if (ColorUtility.TryParseHtmlString(ParsedSource.color, out parsedColor))
                Color = parsedColor;
            MapPosition = new Vector3(ParsedSource.posX, ParsedSource.posY, 0);
            
            _objects = new List<RegionObjectData>();

            foreach (var objectSource in ParsedSource.objects)
            {
                AddRegionData(objectSource);
            }
        }

        private void AddRegionData(RegionObjectSource source)
        {
            var data = new RegionObjectData();
            data.SetParsedSource(source);
            _objects.Add(data);
            OnRegionObjectDataAdded?.Invoke(data);
        }
    }
}