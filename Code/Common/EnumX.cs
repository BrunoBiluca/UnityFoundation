using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnumX<T> where T : EnumX<T> {
    protected int index;
    protected string name;
    protected static readonly Dictionary<int, EnumX<T>> values = new Dictionary<int, EnumX<T>>();

    public EnumX(int index, string name) {
        this.index = index;
        this.name = name;
        values.Add(index, this);
    }

    public static implicit operator int(EnumX<T> value) => value.index;

    public static implicit operator EnumX<T>(int index) =>
        values.TryGetValue(index, out var question) ? question : null;

    public override string ToString() =>
        this.name;

    public static implicit operator string(EnumX<T> value) =>
        value?.ToString();

    public static implicit operator EnumX<T>(string name) => name == null 
        ? null 
        : values.Values.FirstOrDefault(
            item => name.Equals(item.name, StringComparison.CurrentCultureIgnoreCase)
        );

    public EnumX<T> Get(int foo) => foo;
}
