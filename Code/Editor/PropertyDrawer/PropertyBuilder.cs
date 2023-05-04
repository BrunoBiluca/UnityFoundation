using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityFoundation.Editor
{
    public class FoldoutGroup
    {
        private readonly List<Action> properties = new();
        private readonly bool isFoldout;

        public FoldoutGroup(bool isFoldout)
        {
            this.isFoldout = isFoldout;
        }

        public void Add(Action callback)
        {
            properties.Add(callback);
        }

        public void End()
        {
            if(isFoldout)
                foreach(var a in properties)
                    a();

            EditorGUI.EndFoldoutHeaderGroup();
        }
    }

    public class PropertyBuilder
    {
        public float ItemSize { get; set; }
        public float Height => ItemSize * fieldOrder;

        private Rect position;
        private int fieldOrder = 0;

        public PropertyBuilder(Rect position)
        {
            this.position = position;
        }

        public void FloatField(string label, float value, Action<float> callback)
        {
            var newValue = EditorGUI.FloatField(GetFieldPosition(), label, value);
            callback(newValue);
        }

        public void Toggle(string label, bool value, Action<bool> callback)
        {
            var newValue = EditorGUI.Toggle(GetFieldPosition(), label, value);
            callback(newValue);
        }

        public FoldoutGroup FoldoutGroud(string label, bool isFoldout, Action<bool> callback)
        {
            var newIsFoldout = EditorGUI.BeginFoldoutHeaderGroup(
                GetFieldPosition(),
                isFoldout,
                label
            );

            callback(newIsFoldout);

            return new FoldoutGroup(isFoldout);
        }

        private Rect GetFieldPosition()
        {
            return new Rect(
                position.x,
                position.y + ItemSize * fieldOrder++,
                position.width,
                ItemSize
            );
        }
    }
}
