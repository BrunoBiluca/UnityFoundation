using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Cursors
{
    public class ScreenPositionConverterGroup : IScreenPositionConverter
    {
        private readonly List<IScreenPositionConverter> converters;

        public ScreenPositionConverterGroup(List<IScreenPositionConverter> converters)
        {
            this.converters = converters;
        }

        public Optional<Vector2> Eval(Vector2 screenPos)
        {
            var currentPos = Optional<Vector2>.Some(screenPos);
            foreach(var converter in converters)
            {
                if(currentPos.IsPresentAndGet(out Vector2 pos))
                    currentPos = converter.Eval(pos);
            }
            return currentPos;
        }
    }
}