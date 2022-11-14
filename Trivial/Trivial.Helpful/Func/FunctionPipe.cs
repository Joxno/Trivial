using System;

namespace Trivial.Helpful;

public static partial class Function
{
    public static Func<T2> Pipe<T1, T2>(Func<T1> A, Func<T1, T2> B) =>
        () => B(A());

    public static Func<T1, T3> Pipe<T1, T2, T3>(Func<T1, T2> A, Func<T2, T3> B) =>
        (P) => B(A(P));

    public static Func<T1, TR> Pipe<T1, T2, T3, TR>(Func<T1, T2> A, Func<T2, T3> B, Func<T3, TR> C) =>
        (P) => C(B(A(P)));

    public static Func<T1, TR> Pipe<T1, T2, T3, T4, TR>
        (Func<T1, T2> A, Func<T2, T3> B, Func<T3, T4> C, Func<T4, TR> D) =>
        (P) => D(C(B(A(P))));

    public static Func<T1, TR> Pipe<T1, T2, T3, T4, T5, TR>
        (Func<T1, T2> A, Func<T2, T3> B, Func<T3, T4> C, Func<T4, T5> D, Func<T5, TR> E) =>
        (P) => E(D(C(B(A(P)))));

    public static Func<T1, TR> Pipe<T1, T2, T3, T4, T5, T6, TR>
        (Func<T1, T2> A, Func<T2, T3> B, Func<T3, T4> C, Func<T4, T5> D, Func<T5, T6> E, Func<T6, TR> F) =>
        (P) => F(E(D(C(B(A(P))))));

    public static Func<T1, TR> Pipe<T1, T2, T3, T4, T5, T6, T7, TR>
        (Func<T1, T2> A, Func<T2, T3> B, Func<T3, T4> C, Func<T4, T5> D, 
        Func<T5, T6> E, Func<T6, T7> F, Func<T7, TR> G) =>
        (P) => G(F(E(D(C(B(A(P)))))));

    public static Func<T1, T3> PipeTo<T1, T2, T3>(this Func<T1, T2> A, Func<T2, T3> B) =>
        (P) => B(A(P));

    public static Func<T1, T2> Pipe<T1, T2>(Func<T1, T2> A, Action<T2> B) =>
        (P) => Tap(B)(A(P));

    public static Func<T1, T2> Pipe<T1, T2>(Action<T1> A, Func<T1, T2> B) =>
        (P) => B(Tap(A)(P));

    public static Func<T1, T2> Pipe<T1, T2>(Func<T1, T1> Id, Func<T2> A) =>
        (P) => { Id(P); return A(); };

    public static Func<T, T> Tap<T, T2>(Func<T, T2> Func) =>
        (P) => { Func(P); return P; };

    public static Func<T, T> Tap<T>(Action<T> Func) =>
        (P) => { Func(P); return P; };
}
