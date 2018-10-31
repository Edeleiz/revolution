using Code.Game.Map.Base;
using Code.Game.Map.Source;
using UnityEngine;

namespace Code.Game.Map.Data
{
    public class RegionData : BaseData<RegionSource>
    {
        public Color Color { get; private set; }
        public string Name { get; private set; }
        
        protected override void UpdateSource()
        {
            Name = ParsedSource.name;
            var parsedColor = default(Color);
            if (ColorUtility.TryParseHtmlString(ParsedSource.color, out parsedColor))
                Color = parsedColor;
        }
    }
}