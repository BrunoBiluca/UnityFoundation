using System.Reflection;
using UnityEngine;

namespace UnityFoundation.TestUtility
{
    public class CollisionFactory
    {

        public static Collision Create(MonoBehaviour objMono)
        {
            var collision = new Collision();
            var field = typeof(Collision).GetProperty(
                "body", BindingFlags.Instance | BindingFlags.NonPublic
            );
            field.SetValue(collision, objMono); // Set non-public field
            return collision;
        }

    }

    public class CollisionTestHelper : Collision
    {
        public virtual new GameObject gameObject { get; set; }
    }
}
