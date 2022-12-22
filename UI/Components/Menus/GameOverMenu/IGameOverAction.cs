using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.UI
{
    public interface IGameOverAction
    {
        string Name { get; }

        void Execute();
    }
}