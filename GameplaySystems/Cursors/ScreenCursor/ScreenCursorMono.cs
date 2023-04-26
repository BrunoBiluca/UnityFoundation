using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;
using UnityFoundation.Cursors;

namespace UnityFoundation.Cursors
{
    public class ScreenCursorMono : ComponentHolder<ScreenCursor>
    {
        [field: SerializeField] public bool CheckBoundaries { get; set; }
        [field: SerializeField] public bool UsePanelAsPivot { get; set; }
        [SerializeField] RectTransform Panel;

        public RectTransform Pivot => Panel;

        /// <summary>
        /// Use when canvas scaler has fixed reference resolution
        /// </summary>
        [field: SerializeField] public bool UseCanvasResolutionReference { get; set; }
        [SerializeField] private Canvas canvas;

        protected override ScreenCursor Create()
        {
            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
            var referenceResolution = scaler.referenceResolution;
            var screenScaler = new ScreenScaler(referenceResolution);

            var cursor = new ScreenCursor(new MouseInput()) {
                MinDragDistance = 20 * screenScaler.HeightRatio
            };

            var converter = new List<IScreenPositionConverter>();
            if(CheckBoundaries)
            {
                var boundaries = new BoundariesChecker(
                    new(Panel.position, screenScaler.ScaleRectSize(Panel.rect.size))
                ) {
                    Direction = new(1, -1)
                };
                converter.Add(boundaries);
            }

            if(UsePanelAsPivot)
            {
                converter.Add(new PivotConverter(Panel.position));
            }

            if(UseCanvasResolutionReference)
            {
                converter.Add(new CanvasResolutionConverter(referenceResolution));
            }

            cursor.SetConverter(new ScreenPositionConverterGroup(converter));
            return cursor;
        }
    }
}