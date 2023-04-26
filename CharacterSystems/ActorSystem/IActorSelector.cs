using System;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface ICurrentActorSelected<TActor>
    {
        public TActor CurrentUnit { get; }
    }

    public interface IActorSelector<TActor> : ICurrentActorSelected<TActor>
    {
        event Action OnUnitSelected;
        event Action OnUnitUnselected;

        void UnselectUnit();
    }
}