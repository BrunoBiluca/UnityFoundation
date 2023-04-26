using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.DiceSystem
{
    public abstract partial class AbstractDiceMono<T>
    {
        public class ObjectDiceSide : DiceSide<T>
        {
            private readonly ITransform transform;

            public ITransform GetTransform() => transform;

            public ObjectDiceSide(int index, T value, ITransform transform)
                : base(index, value)
            {
                this.transform = transform;
            }
        }
    }
}