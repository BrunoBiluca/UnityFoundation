using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface IRigidbody
    {
        Vector3 Velocity { get; set; }
        Vector3 AngularVelocity { get; set; }

        IRigidbody GetRigidbody();
        void AddForce(Vector3 force);
        void AddForce(Vector3 vector3, ForceMode impulse);
        void AddTorque(float x, float y, float z);
    }
}