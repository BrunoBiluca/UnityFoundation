using System;

namespace UnityFoundation.CharacterSystem.ActorSystem
{
    public interface IActorSelector<TActor>
    {
        public TActor CurrentUnit { get; }

        event Action OnUnitSelected;
        event Action OnUnitUnselected;

        void UnselectUnit();
    }
}