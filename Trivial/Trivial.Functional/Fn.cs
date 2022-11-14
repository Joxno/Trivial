using System;

namespace Trivial.Functional;

public static partial class Functions
{
    public static Func<T> Fn<T>(T Value) => () => Value;
    public static Func<T> Fn<T>(Func<T> F) => () => F();
}