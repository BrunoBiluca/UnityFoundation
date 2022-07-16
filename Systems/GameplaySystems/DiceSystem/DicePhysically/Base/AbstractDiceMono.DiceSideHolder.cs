using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.DiceSystem
{
    public abstract partial class AbstractDiceMono<T>
    {
        public abstract class DiceSideHolder : MonoBehaviour
        {
            [SerializeField] private T value;

            public void Setup(T value)
            {
                this.value = value;
            }

            public ObjectDiceSide GetValue(int index)
            {
                return new ObjectDiceSide(index, value, new TransformDecorator(transform));
            }
        }
    }
}