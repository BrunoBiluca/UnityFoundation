using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code.Guard;

namespace UnityFoundation.Compass
{
    public class CompassController
    {
        public Transform Player { get; private set; }
        public Dictionary<Transform, CompassObject> CompassObjects { get; private set; }

        public event Action<CompassObject> OnRegister;

        public CompassController(Transform player)
        {
            Player = Guard.Required(player, "Player");

            CompassObjects = new Dictionary<Transform, CompassObject>();
        }

        public void Register(Transform obj)
        {
            var trackedObject = new CompassObject(obj);
            CompassObjects.Add(obj, trackedObject);
            OnRegister?.Invoke(trackedObject);
        }

        public void Update()
        {
            if(Player == null) return;

            foreach(var obj in CompassObjects)
            {
                var direction = obj.Value.Obj.position - Player.position;

                var angleToTarget = Vector3.SignedAngle(
                    Player.transform.forward, direction, Player.transform.up);

                angleToTarget = Mathf.Clamp(angleToTarget, -90, 90);

                obj.Value.Angle = angleToTarget;
                obj.Value.Distance = direction.magnitude;
            }
        }
    }
}