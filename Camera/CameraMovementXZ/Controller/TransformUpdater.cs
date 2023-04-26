using System.Collections.Generic;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.CameraMovementXZ
{
    public class TransformUpdater
    {
        private readonly List<ITransformUpdater> updaters = new();
        private readonly ITransform target;

        public TransformUpdater(ITransform transform)
        {
            target = transform;
        }

        public void AddUpdater(ITransformUpdater updater)
        {
            updaters.Add(updater);
        }

        public void Update(float interpolateAmount)
        {
            foreach(var updater in updaters)
                updater.Update(target, interpolateAmount);
        }
    }
}