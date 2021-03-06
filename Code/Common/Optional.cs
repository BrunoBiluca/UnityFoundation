using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optional<T> {

    public static Optional<T> Some(T value) {
        return new Optional<T>(value);
    }

    public static Optional<T> None() {
        return new Optional<T>();
    }

    private readonly T value;

    public Optional() {
        IsPresent = false;
    }

    public Optional(T value) {
        this.value = value;
        IsPresent = true;
    }

    public bool IsPresent { get; private set; }

    public Optional<T> Some(Action<T> action) {
        if(!IsPresent) return this;

        action(value);
        return this;
    }

    public T OrElse(Action action) {
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
}
