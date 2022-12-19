﻿using UnityEngine;

namespace UnityFoundation.Code
{
    public interface IProjectileFactory
    {
        IProjectile Create(Vector3 start, Vector3 target);
    }
}
