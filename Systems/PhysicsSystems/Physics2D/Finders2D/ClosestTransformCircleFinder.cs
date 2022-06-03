using UnityEngine;

namespace UnityFoundation.Physics2D.Finder
{
    public class ClosestTransformCircleFinder : TransformCircleFinder
    {

        protected override void Find()
        {
            var nearObjects = UnityEngine.Physics2D.OverlapCircleAll(
                referenceTransform.position, lookRangeRadius
            );

            var minDistance = float.MaxValue;
            foreach(var obj in nearObjects)
            {
                var searchedComponent = obj.gameObject.GetComponent(lookingForType);

                if(searchedComponent != null)
                {
                    var distance = Vector3.Distance(
                        obj.transform.position, referenceTransform.position
                    );
                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        target = obj.transform;
                    }
                }
            }
        }

    }
}