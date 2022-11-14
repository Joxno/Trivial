using System;

namespace Trivial.Helpful;

public static partial class Functions
{
    public static Func<TR> Let<T, TR>(T Val, Func<T, TR> Scope) => 
        Scope.Apply(Val);
    public static Func<TR> Let<T1, T2, TR>(T1 V1, T2 V2, Func<T1, T2, TR> Scope) => 
        Scope.Apply(V1).Apply(V2);
    public static Func<TR> Let<T1, T2, T3, TR>(T1 V1, T2 V2, T3 V3, Func<T1, T2, T3, TR> Scope) =>
        Scope.Curry().Apply(V1).Apply(V2).Apply(V3);
    public static Func<TR> Let<T1, T2, T3, T4, TR>(T1 V1, T2 V2, T3 V3, T4 V4, Func<T1, T2, T3, T4, TR> Scope) =>
        Scope.Curry().Apply(V1).Apply(V2).Apply(V3).Apply(V4);
}
