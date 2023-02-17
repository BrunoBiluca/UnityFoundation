using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityFoundation.CameraMovementXZ
{
    public class EdgeScreenInputs
    {
        public class Settings
        {
            public float EdgeOffset { get; set; } = 0f;
        }

        public event Action<Vector2> OnEdgeMovement;

        private readonly Settings config;
        private Vector2 position;

        public EdgeScreenInputs(Settings config)
        {
            this.config = config;
        }

        public void Update()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            position = new Vector2(
                EvaluateEdge(mousePosition.x),
                EvaluateEdge(mousePosition.y)
            );

            if(position != Vector2.zero) 
                OnEdgeMovement(position);
        }

        private float EvaluateEdge(float position)
        {
            if(position > Screen.width - config.EdgeOffset)
                return 1f;
            else if(position < config.EdgeOffset)
                return -1f;

            return 0f;
        }
    }
}