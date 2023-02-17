using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.DiceSystem
{
    public abstract partial class AbstractDiceMono<T> : MonoBehaviour, IDiceMono
    {
        [SerializeField]
        private List<DiceSideHolder> holders = new List<DiceSideHolder>();

        private List<ObjectDiceSide> sides;
        private TransformDecorator transformDec;
        private RigidbodyDecorator rigidbodyDec;

        public int SidesCount => sides.Count;
        public IDiceSide[] Sides => sides.ToArray();
        public ITransform GetTransform() => transformDec;
        public IRigidbody GetRigidbody() => rigidbodyDec;

        public void Awake()
        {
            transformDec = new TransformDecorator(transform);
            rigidbodyDec = new RigidbodyDecorator(GetComponent<Rigidbody>());
            sides = new List<ObjectDiceSide>();

            UpdateDiceSides();
        }

        public void AddSideHolder(DiceSideHolder holder)
        {
            holders.Add(holder);
        }

        public void UpdateDiceSides()
        {
            foreach(var holder in holders)
            {
                var sideValue = holder.GetValue(sides.Count);
                sides.Add(sideValue);
            }
        }

        public ObjectDiceSide GetSide(IDiceSide side)
        {
            return sides
                .Where(s => s.GetIndex() == side.GetIndex())
                .FirstOrDefault();
        }

        public T GetValue(IDiceSide side)
        {
            return GetSide(side).GetValue();
        }

        public IDiceSide CheckSelectedSide()
        {
            foreach(var side in sides)
            {
                // side forward is facing the dice's center
                var sideUp = side.GetTransform().Forward.normalized;
                var isFacingUp = Vector3.Dot(sideUp, Vector3.up) == -1;
                if(isFacingUp)
                    return side;
            }

            throw new InvalidOperationException(
                "There is no sides facing up when dice was thrown");
        }
    }
}