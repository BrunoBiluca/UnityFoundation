using Assets.UnityFoundation.DebugHelper;
using Assets.UnityFoundation.GameManagers;
using Assets.UnityFoundation.TimeUtils;
using System;
using UnityEngine;

namespace Assets.UnityFoundation.Code {
    public class TransformCircleFinder : MonoBehaviour {

        [SerializeField]
        protected Transform defaultTarget;

        protected Type lookingForType;
        protected float lookRangeRadius;
        protected Timer lookForTargetsTimer;
        protected Transform referenceTransform;

        protected Transform target;
        public Optional<Transform> Target {
            get {
                return target != null ? Optional<Transform>.Some(target) : Optional<Transform>.None();
            }
        }

        public virtual void Setup(
            Type lookingForType,
            Transform referenceTransform = null,
            float lookRangeRadius = 6f,
            Transform defaultTarget = null
        ) {
            this.defaultTarget = defaultTarget;
            this.lookingForType = lookingForType;
            this.lookRangeRadius = lookRangeRadius;

            if(referenceTransform == null) {
                this.referenceTransform = transform;
            } else {
                this.referenceTransform = referenceTransform;
            }

            lookForTargetsTimer = new Timer(
                $"Look for targets {lookingForType}", 0.2f, Find
            );
        }

        void Update() {
            if(BaseGameManager.Instance.DebugMode) {
                DebugDraw.DrawCircle(transform.position, lookRangeRadius, Color.green);
            }
        }

        protected virtual void Find() {
            var nearObjects = Physics2D.OverlapCircleAll(
                referenceTransform.position, lookRangeRadius
            );

            foreach(var obj in nearObjects) {
                var searchedComponent = obj.gameObject.GetComponent(lookingForType);
                if(searchedComponent != null) {
                    target = obj.transform;
                    return;
                }
            }

            target = defaultTarget;
        }

        private void OnDestroy() {
            if(lookForTargetsTimer != null)
                lookForTargetsTimer.Close();
        }
    }
}