using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Features.Merge.Screens
{
    [Serializable]
    public class SystemData
    {
        public bool IsBlocked;
        public string Description;
        public List<Sprite> Sprites = new();
        public ActionData ActionData;
        public int Timer;
    }
}