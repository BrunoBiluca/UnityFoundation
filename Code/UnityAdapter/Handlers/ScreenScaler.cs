using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class ScreenScaler
    {
        private float width;
        private float height;
        private readonly float heightReference;

        public float HeightRatio => height / heightReference;

        public ScreenScaler(Vector2 screenReference)
        {
            width = Screen.width;
            height = Screen.height;
            heightReference = screenReference.y;
        }

        public Vector2 ScaleRectSize(Vector2 size)
        {
            return size * HeightRatio;
        }
    }
}
