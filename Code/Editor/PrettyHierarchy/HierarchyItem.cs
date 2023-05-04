using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Editor.Hierarchy;

namespace UnityFoundation.Editor.PrettyHierarchy
{
    public class HierarchyItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsSelected { get; private set; }
        public bool IsHover { get; private set; }
        public bool HasCollapseToggle { get; private set; }
        public bool IsCollapsed { get; private set; }
        public bool IsInEditMode { get; private set; }
        public Color BackgroundColor { get; private set; }
        public Color FontColor { get; private set; }
        public Rect Rect { get; private set; }
        public Rect TextRect { get; private set; }
        public Rect EditPrefabIconRect { get; private set; }
        public Rect CollapseToggleIconRect { get; private set; }
        public Rect PrefabIconRect { get; private set; }

        public HierarchyItem(int instanceID, Rect selectionRect, PrettyObject obj)
        {
            Id = instanceID;
            IsSelected = Selection.Contains(instanceID);
            IsActive = obj.GameObject.activeInHierarchy;
            HasCollapseToggle = obj.GameObject.transform.childCount > 0;
            IsCollapsed = GetIsCollapsed(instanceID);

            IsInEditMode = PrefabUtility
                .GetCorrespondingObjectFromOriginalSource(obj.GameObject) != null
                && PrefabUtility.IsAnyPrefabInstanceRoot(obj.GameObject);

            Name = obj.GameObject.name;

            CreateReacts(selectionRect);

            BackgroundColor = GetBackgroundColor(obj);
            FontColor = GetFontColor(obj);

            IsHover = Rect.Contains(Event.current.mousePosition);
        }

        private void CreateReacts(Rect selectionRect)
        {
            var hierarchySideBarOffset = 32f;
            float xPos = selectionRect.position.x
                - selectionRect.xMin
                + hierarchySideBarOffset;
            float yPos = selectionRect.position.y;
            float xSize = selectionRect.size.x + selectionRect.xMin;
            float ySize = selectionRect.size.y;
            Rect = new Rect(xPos, yPos, xSize, ySize);

            xPos = selectionRect.position.x + 18f;
            yPos = selectionRect.position.y;
            xSize = selectionRect.size.x - 18f;
            ySize = selectionRect.size.y;
            TextRect = new Rect(xPos, yPos, xSize, ySize);

            xPos = selectionRect.position.x - 14f;
            yPos = selectionRect.position.y + 1f;
            xSize = 13f;
            ySize = 13f;
            CollapseToggleIconRect = new Rect(xPos, yPos, xSize, ySize);

            xPos = selectionRect.position.x;
            yPos = selectionRect.position.y;
            xSize = 16f;
            ySize = 16f;
            PrefabIconRect = new Rect(xPos, yPos, xSize, ySize);

            xPos = Rect.xMax - 16f;
            yPos = selectionRect.yMin;
            xSize = 16f;
            ySize = 16f;
            EditPrefabIconRect = new Rect(xPos, yPos, xSize, ySize);
        }

        private Color GetBackgroundColor(PrettyObject obj)
        {
            if(IsSelected)
                return EditorColors.GetDefaultSelectedBackgroundColor();

            if(obj.UseDefault)
                return EditorColors.Background;

            return obj.BackgroundColor;
        }

        private Color GetFontColor(PrettyObject obj)
        {
            if(IsSelected)
            {
                return EditorColors
                    .GetDefaultSelectedTextColor(obj.GameObject.activeInHierarchy);
            }

            if(obj.UseDefault)
                return EditorColors.Text;

            return obj.FontColor;
        }

        private bool GetIsCollapsed(int instanceID)
        {
            var sceneHierarchyWindowType = typeof(UnityEditor.Editor).Assembly
                .GetType("UnityEditor.SceneHierarchyWindow");
            var sceneHierarchyWindow = sceneHierarchyWindowType
                .GetProperty(
                    "lastInteractedHierarchyWindow",
                    BindingFlags.Public | BindingFlags.Static
                );

            int[] expandedIDs = (int[])sceneHierarchyWindowType
                .GetMethod(
                    "GetExpandedIDs",
                    BindingFlags.NonPublic | BindingFlags.Instance
                )
                .Invoke(sceneHierarchyWindow.GetValue(null), null);

            return expandedIDs.Contains(instanceID);
        }
    }
}