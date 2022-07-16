using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class RigidbodyDecorator : IRigidbody
    {
        public Rigidbody Rigidbody { get; }
        public IRigidbody GetRigidbody() => this;

        public RigidbodyDecorator(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;
        }

        public Vector3 Velocity {
            get => Rigidbody.velocity;
            set => Rigidbody.velocity = value;
        }
        public Vector3 AngularVelocity {
            get => Rigidbody.angularVelocity;
            set => Rigidbody.angularVelocity = value;
        }

        public void AddForce(Vector3 force)
            => Rigidbody.AddForce(force);

        public void AddForce(Vector3 vector3, ForceMode impulse)
            => Rigidbody.AddForce(vector3, impulse);

        public void AddTorque(float x, float y, float z)
        {
            Rigidbody.AddTorque(x, y, z);
        }
    }
}