using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.ResourceManagement;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IAPActor : IActor<IAPIntent>
    {
        IResourceManager ActionPoints { get; }
    }
}
