using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Code.CameraScripts.OldInputSystem
{
    public class EdgeScrolling : MonoBehaviour
    {

        [SerializeField]
        private float edgeOffset;
        public float EdgeOffset {
            get { return edgeOffset; }
            set { edgeOffset = value; }
        }

        [SerializeField]
        private float movimentSpeed = 30f;
        public float MovimentSpeed {
            get { return movimentSpeed; }
            set { movimentSpeed = value; }
        }

        public bool enable = true;

        void Update()
        {
            if(enable)
            {
                MoveCamera();
            }
        }

        private void MoveCamera()
        {
            transform.position += new Vector3(
                XPosition() * Time.deltaTime,
                YPosition() * Time.deltaTime,
                0f
            );
        }

        private float XPosition()
        {
            if(Input.mousePosition.x > Screen.width - edgeOffset)
            {
                return movimentSpeed;
            }
            else if(Input.mousePosition.x < edgeOffset)
            {
                return -movimentSpeed;
            }

            return 0f;
        }

        private float YPosition()
        {
            if(Input.mousePosition.y > Screen.height - edgeOffset)
            {
                return movimentSpeed;
            }
            else if(Input.mousePosition.y < edgeOffset)
            {
                return -movimentSpeed;
            }

            return 0f;
        }


        public void Disable()
        {
            Destroy(this);
        }
    }
}