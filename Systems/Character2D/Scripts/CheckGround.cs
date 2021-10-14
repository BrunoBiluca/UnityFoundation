using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D
{
    public class CheckGround : MonoBehaviour
    {
        private const string groundTag = "ground";
        private Player player;

        private void Start()
        {
            player = GetComponentInParent<Player>();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if(!collision.gameObject.CompareTag(groundTag)) return;

            player.IsOnGround = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(!collision.gameObject.CompareTag(groundTag)) return;

            player.IsOnGround = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if(!collision.gameObject.CompareTag(groundTag)) return;

            player.IsOnGround = false;
        }
    }
}