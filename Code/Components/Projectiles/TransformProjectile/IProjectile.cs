using System;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public interface IProjectile
    {
        public class Settings
        {
            public ITransform Transform { get; set; }
            public float Speed { get; set; }
            public Vector3 TargetPos { get; set; }
        }

        event Action OnReachTarget;

        void Update(float interpolateTime = 1);
        void Setup(Settings config);
    }
}
