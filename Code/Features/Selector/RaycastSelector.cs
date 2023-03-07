using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public sealed class RaycastSelector : ISelector
    {
        private readonly IRaycastHandler raycast;
        private LayerMask layerMask;

        private Optional<ISelectable> currentUnit;
        public Optional<ISelectable> CurrentUnit {
            get {
                if(
                    !currentUnit.IsPresentAndGet(out ISelectable value)
                    || IsNullOrDestroyed(value)
                )
                {
                    currentUnit = Optional<ISelectable>.None();
                }

                return currentUnit;
            }
            private set { currentUnit = value; }
        }

        public RaycastSelector(IRaycastHandler raycast)
        {
            this.raycast = raycast;
            layerMask = 0;
            CurrentUnit = Optional<ISelectable>.None();
        }

        public RaycastSelector SetLayers(LayerMask layerMask)
        {
            this.layerMask = layerMask;
            return this;
        }

        public Optional<ISelectable> Select(Vector3 screenPosition)
        {
            Unselect();

            var target = raycast.GetObjectOf<ISelectable>(screenPosition, layerMask);

            var result = Optional<ISelectable>.Some(target);
            if(result.IsPresent)
            {
                result.Get().SetSelected(true);
            }

            CurrentUnit = result;
            return result;
        }

        public Optional<T> Select<T>(Vector3 screenPosition) where T : ISelectable
        {
            var unit = Select(screenPosition);
            if(!unit.IsPresentAndGet(out ISelectable selected))
            {
                return Optional<T>.None();
            }
            return Optional<T>.Some((T)selected);
        }

        public static bool IsNullOrDestroyed(object obj)
        {
            if(obj is null) return true;
            return obj is Object && obj as Object == null;
        }

        public void Unselect()
        {
            CurrentUnit.Some(u => u.SetSelected(false));
            CurrentUnit = Optional<ISelectable>.None();
        }
    }
}