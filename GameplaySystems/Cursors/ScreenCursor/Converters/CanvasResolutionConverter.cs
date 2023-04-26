using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Cursors
{
    public class CanvasResolutionConverter : IScreenPositionConverter
    {
        private readonly float widthRatio;
        private readonly float heightRatio;

        public CanvasResolutionConverter(Vector2 resolution)
        {
            widthRatio = Screen.width / resolution.x;
            heightRatio = Screen.height / resolution.y;

            Debug.Log("Screen resolution - Width: " + Screen.width + " - Height: " + Screen.height);
            Debug.Log("Aspect ratio: width => " + widthRatio + " height => " + heightRatio);
        }

        public Optional<Vector2> Eval(Vector2 screenPos)
        {
            return Optional<Vector2>.Some(screenPos / heightRatio);
        }
    }
}
