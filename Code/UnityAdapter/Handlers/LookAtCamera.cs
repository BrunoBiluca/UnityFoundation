using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class LookAtCamera : MonoBehaviour
    {
        public void Update()
        {
            if(Camera.main == null) return;
            transform.LookAt(Camera.main.transform);
        }
    }
}
