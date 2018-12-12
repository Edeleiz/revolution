using Code.Game.Map.Base;
using Code.Game.Map.Source;
using UnityEngine;

public class RegionObjectData : BaseData<RegionObjectSource>
{
    public string Name { get; private set; }
    public Vector3 MapPosition { get; private set; }
    
    protected override void UpdateSource()
    {
        Name = ParsedSource.name;
        MapPosition = new Vector3(ParsedSource.posX, ParsedSource.posY, 0);
    }
}