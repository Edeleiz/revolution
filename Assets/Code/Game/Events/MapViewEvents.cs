using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewEvents
{
    private static MapViewEvents _instance;
    public static MapViewEvents Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MapViewEvents();
            return _instance;
        }
    }
    
    public Action<Color> OnRegionClicked;
    public Action<Color> OnRegionHovered;
}
