using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.Helpful
{
    public struct Maybe<T>
    {
        private TypeWrapper<T> m_Value;

        public Maybe(T Value) => 
            m_Value = 
            Value != null ? 
            new TypeWrapper<T> { WrappedValue = Value } : 
            null;

        public bool HasValue => m_Value != null;

        public T Value => !HasValue ? throw new NullReferenceException() : m_Value.WrappedValue;

        public static implicit operator Maybe<T>(T Value) => Value != null ? new Maybe<T>(Value) : new Maybe<T>();
        public static implicit operator Maybe<T>(NoValue NoVal) => new Maybe<T>();

        /* Container for wrapping types 
         * in order to support class, struct and interface types in Maybe */
        private class TypeWrapper<Type>
        {
            public Type WrappedValue { get; set; }

            public static implicit operator TypeWrapper<Type>(Type Value) =>
                new TypeWrapper<Type> { WrappedValue = Value };
        }

        /* >>= */
        public static Maybe<T2> Bind<T2>(Maybe<T> Value, Func<T, Maybe<T2>> Func) =>
            Value.HasValue ? Func(Value.m_Value.WrappedValue) : null;

        /* >> */
        public static Maybe<T2> Bind<T2>(Maybe<T> Value, Func<Maybe<T2>> Func) =>
            Bind(Value, Function.Pipe(Functions.Identity<T>(), () => Func()));

        /* >>= */
        public Maybe<T2> Bind<T2>(Func<T, Maybe<T2>> Func) =>
            Bind(this, Func);

        /* >> */
        public Maybe<T2> Bind<T2>(Func<Maybe<T2>> Func) =>
            Bind(this, Func);

        /* Select */
        public Maybe<T2> Map<T2>(Func<T, T2> Func) =>
            Bind(this, (P) => new Maybe<T2>(Func(P)));
    }

    public class NoValue { }

    public static class Maybe
    {
        public static Maybe<T> Some<T>(T Value) => Value;
        public static NoValue Null => new NoValue();
        public static NoValue None => Null;
        public static Func<Maybe<T>> Lift<T>(Func<T> Func) => () => Func();
        public static Maybe<T> Return<T>(T Value) => Value;

    }
    
}
