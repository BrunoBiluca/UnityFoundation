using UnityEditor;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Editor.Hierarchy;
using Color = UnityEngine.Color;

namespace UnityFoundation.Editor.PrettyHierarchy
{
    [InitializeOnLoad]
    public static class PrettyHierarchy
    {
        static PrettyHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyItem;
        }

        private static void DrawHierarchyItem(int instanceID, Rect selectionRect)
        {
            var instance = EditorUtility.InstanceIDToObject(instanceID);

            if(instance is not GameObject go) return;

            if(!go.TryGetComponent(out IPrettyable prettyObj)) return;

            var item = new HierarchyItem(instanceID, selectionRect, prettyObj.BePretty());

            DrawBackground(item);
            DrawText(item);
            DrawCollapseToggleIcon(item);
            DrawPrefab(item);
            DrawEditPrefab(item);
        }

        private static void DrawBackground(HierarchyItem item)
        {
            EditorGUI.DrawRect(item.Rect, item.BackgroundColor);

            if(item.IsHover)
            {
                EditorGUI.DrawRect(item.Rect, EditorColors.HoverOverlay);
            }
        }

        private static void DrawText(HierarchyItem item)
        {
            var style = new GUIStyle() {
                normal = new GUIStyleState() { textColor = item.FontColor }
            };

            EditorGUI.LabelField(item.TextRect, item.Name, style);
        }

        private static void DrawCollapseToggleIcon(HierarchyItem item)
        {
            if(!item.HasCollapseToggle) return;

            var icon = item.IsCollapsed ? "IN Foldout on" : "IN foldout";
            GUI.DrawTexture(
                item.CollapseToggleIconRect,
                EditorGUIUtility.IconContent(icon).image,
                ScaleMode.StretchToFill,
                alphaBlend: true,
                imageAspect: 0f,
                EditorColors.CollapseIconTintColor,
                borderWidth: 0f,
                borderRadius: 0f
            );
        }

        private static void DrawPrefab(HierarchyItem item)
        {
            var icon = EditorGUIUtility
                .ObjectContent(EditorUtility.InstanceIDToObject(item.Id), null)
                .image;

            if(EditorUtils.IsHierarchyFocused && item.IsSelected)
            {
                if(icon.name == "d_Prefab Icon" || icon.name == "Prefab Icon")
                {
                    icon = EditorGUIUtility.IconContent("d_Prefab On Icon").image;
                }
                else if(icon.name == "GameObject Icon")
                {
                    icon = EditorGUIUtility.IconContent("GameObject On Icon").image;
                }
            }

            var iconColor = item.IsActive ? Color.white : new Color(1f, 1f, 1f, 0.5f);

            GUI.DrawTexture(
                item.PrefabIconRect,
                icon,
                ScaleMode.StretchToFill,
                alphaBlend: true,
                imageAspect: 0f,
                iconColor,
                borderWidth: 0f,
                borderRadius: 0f
            );
        }

        private static void DrawEditPrefab(HierarchyItem item)
        {
            if(!item.IsInEditMode) return;

            var icon = EditorGUIUtility.IconContent("ArrowNavigationRight").image;
            GUI.DrawTexture(
                item.EditPrefabIconRect,
                icon,
                ScaleMode.StretchToFill,
                alphaBlend: true,
                imageAspect: 0f,
                EditorColors.EditPrefabIconTintColor,
                borderWidth: 0f,
                borderRadius: 0f
            );
        }
    }
}