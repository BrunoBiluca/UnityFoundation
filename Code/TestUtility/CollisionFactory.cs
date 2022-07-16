using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UnityFoundation.TestUtility
{
    public class CollisionFactory
    {

        public static Collision Create(MonoBehaviour objMono)
        {
            var collision = new Collision();
            var field = typeof(Collision).GetField(
                "m_Body", BindingFlags.Instance | BindingFlags.NonPublic
            );
            field.SetValue(collision, objMono); // Set non-public field
            return collision;
        }

    }
}
