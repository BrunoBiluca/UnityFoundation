using Assets.UnityFoundation.Code.Common;
using UnityEngine;

namespace Assets.UnityFoundation.CameraScripts {
    public class CameraFollower : Singleton<CameraFollower> {
        private float offsetX;

        private IFollowable follower;

        public void Setup(IFollowable follower) {
            this.follower = follower;

            offsetX = follower.GetPositionOffset().x;
        }

        public void Update() {
            MoveTheCamera();
        }

        void MoveTheCamera() {
            if(follower == null) return;
            if(follower.StopFollow()) return;

            Vector3 temp = transform.position;
            temp.x = follower.GetPosition().x + offsetX;
            transform.position = temp;
        }
    }
}