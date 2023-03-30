using System;

namespace UnityFoundation.Code
{
    public partial class Optional<T>
    {
        private T value;

        public Optional()
        {
            IsPresent = false;
        }

        public Optional(T value)
        {
            this.value = value;
            IsPresent = value != null;
        }

        public bool IsPresent { get; private set; }

        public bool IsPresentAndGet(out T value)
        {
            value = this.value;
            return IsPresent;
        }

        public Optional<T> Some(Action<T> action)
        {
            if(!IsPresent) return this;

            action(value);
            return this;
        }

        public Optional<T> Some(Func<T, T> func)
        {
            if(!IsPresent) return this;

            value = func(value);
            return this;
        }

        public Optional<T> SomeCopy(Func<T, T> func)
        {
            if(!IsPresent) return this;
            return Some(func(value));
        }

        public T OrElse(Action action)
        {
            if(IsPresent) return value;

            action();
            return value;
        }

        public T OrElse(T defaultValue)
        {
            if(IsPresent) return value;

            return defaultValue;
        }

        /// <summary>
        /// Return the value of the optional.
        /// </summary>
        /// <remarks>Check using IsPresent first to avoid null behaviour.</remarks>
        /// <returns></returns>
        public T Get()
        {
            return value;
        }

        public override string ToString()
        {
            if(IsPresent)
                return "Present: " + value.ToString();
            else
                return $"None of type {typeof(T)}";
        }
    }

    public partial class Optional<T>
    {
        public static implicit operator bool?(Optional<T> optional)
        {
            return optional.IsPresent;
        }

        public static Optional<T> Some(T value)
        {
            return new Optional<T>(value);
        }

        public static Optional<T> None()
        {
            return new Optional<T>();
        }
    }
}