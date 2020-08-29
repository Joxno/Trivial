using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;

namespace Trivial.Helpful.Experimental
{ 
    
    public interface IMonad<T, TM>
    {
        public IMonad<T2, TM> Return<T2>(T2 Value);
        public IMonad<T2, TM> Bind<T2>(Func<T, IMonad<T2, TM>> Func);
        public IMonad<T2, TM> Bind<T2>(Func<IMonad<T2, TM>> Func) => Bind(Function.Pipe(Helpful.Functions.Identity<T>(), () => Func()));
        public IMonad<T2, TM> Map<T2>(Func<T, T2> Func) => Bind((P) => Return(Func(P)));
        public void Then(Action<T> Func);
    }

    public static class IMonadExtensions
    {
        public static IMonad<V, TM> SelectMany<T, TM, U, V>
            (this IMonad<T, TM> M, Func<T, IMonad<U, TM>> Func, Func<T, U, V> S) 
            => M.Bind(X => Func(X).Bind(Y => M.Return(S(X, Y))));
    }

    public class StateGeneric { protected StateGeneric() { } }
    public class StateGeneric<T> : StateGeneric, IMonad<T, StateGeneric>
    {
        private T m_State;

        public StateGeneric(T State) => m_State = State;

        public IMonad<T2, StateGeneric> Bind<T2>(Func<T, IMonad<T2, StateGeneric>> Func) =>
            Func(m_State);

        public IMonad<T2, StateGeneric> Return<T2>(T2 Value) =>
            new StateGeneric<T2>(Value);

        public void Then(Action<T> Func) =>
            Func(m_State);

        public static Func<StateGeneric, StateGeneric<T>> Lift<T>(Func<T> Func) =>
            _ => new StateGeneric<T>(Func());
    }

    public class StateTest
    {
        public void Test()
        {
            new StateGeneric<int>(5)
                .Bind<int>((int i) => new StateGeneric<int>(i))
                .Map((int i) => i)
                .Map((int i) => (float)i + 0.5f)
                .Map((float f) => (double)f)
                .Map((double d) => (int)d)
                .Then(p => Console.WriteLine(p));
        }
    }
    

}
