using UnityEngine;

namespace Assets.UnityFoundation.CameraScripts {
    public class CameraFollower : MonoBehaviour {
        public static CameraFollower Instance { get; private set; }

        public static float offsetX;

        [SerializeField, SerializeReference]
        private IFollowable follower;

        public void Awake() {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Setup(IFollowable follower) {
            this.follower = follower;

            offsetX = follower.GetPositionOffset().x;
        }

        void Update() {
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