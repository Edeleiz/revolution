﻿using System;
using Code.Game.Map.Base;
using UnityEngine;

namespace Code.Game.Map.Source
{
    [Serializable]
    public class RegionSource
    {
        public string name;
        public string color;

        public float posX;
        public float posY;

        public RegionObjectSource[] objects;
    }
}