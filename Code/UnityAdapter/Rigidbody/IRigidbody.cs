using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.UnityAdapter
{
    public interface IRigidbody
    {
        void AddForce(Vector3 force);
        void AddForce(Vector3 vector3, ForceMode impulse);
    }
}