using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Functional;

namespace Trivial.Utilities
{
    public static class MaybeExtensions
    {
        public static void Then<T>(this Maybe<T> M, Action<T> ThenMethod)
        {
            if (M.HasValue)
                ThenMethod?.Invoke(M.Value);
        }

        public static Maybe<T2> Then<T1, T2>(this Maybe<T1> M, Func<T1, T2> ThenMethod)
        {
            if (M.HasValue)
                return Try.GetFrom(ThenMethod)(M.Value);

            return new Maybe<T2>();
        }

        public static Maybe<T> ToMaybe<T>(this T Value) => Value;

        public static Maybe<T2> Select<T, T2>(this Maybe<T> M, Func<T, T2> Func) =>
            M.Map(Func);

        public static Maybe<T3> SelectMany<T, T2, T3>(this Maybe<T> M, Func<T, Maybe<T2>> Func, Func<T, T2, T3> S) =>
            M.Bind(X => Func(X).Bind(Y => Maybe.Return(S(X, Y))));

        public static Maybe<IEnumerable<T>> Raise<T>(this IEnumerable<Maybe<T>> Enumerable) =>
            Enumerable.All(M => M.HasValue)
                    ? new Maybe<IEnumerable<T>>(Enumerable.Select(M => M.Value))
                    : Maybe.Null;

        public static Maybe<List<T>> Raise<T>(this List<Maybe<T>> Enumerable) =>
            Enumerable.All(M => M.HasValue)
                    ? new Maybe<List<T>>(Enumerable.Select(M => M.Value).ToList())
                    : Maybe.Null;

        public static Maybe<ImmutableList<T>> Raise<T>(this ImmutableList<Maybe<T>> Enumerable) =>
            Enumerable.All(M => M.HasValue)
                    ? new Maybe<ImmutableList<T>>(Enumerable.Select(M => M.Value).ToImmutableList())
                    : Maybe.Null;
    }
}
