using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.UnityAdapter
{
    public class RidigbodyDecorator : IRigidbody
    {
        public Rigidbody Rigidbody { get; }

        public RidigbodyDecorator(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;
        }

        public void AddForce(Vector3 force) 
            => Rigidbody.AddForce(force);

        public void AddForce(Vector3 vector3, ForceMode impulse) 
            => Rigidbody.AddForce(vector3, impulse);
    }
}