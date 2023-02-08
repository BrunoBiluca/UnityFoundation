using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class LookAtCamera : MonoBehaviour
    {
        public void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
