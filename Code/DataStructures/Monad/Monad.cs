using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class Monad<T>
    {
        public static Monad<T> Init(T value) => new(value);

        private T value;

        public Monad(T value)
        {
            this.value = value;
        }

        public Monad<T> Map(Func<T, T> transformation)
        {
            value = transformation(value);
            return this;
        }

        public Monad<T> MapIf(Func<T, bool> condition, Func<T, T> transformation)
        {
            if(condition(value))
                value = transformation(value);
            return this;
        }


        public Monad<OT> Chain<OT>(Func<T, OT> chain)
        {
            return new Monad<OT>(chain(value));
        }

        public T Returns() => value;
    }
}
