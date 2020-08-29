using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.Helpful
{
    public class State<T>
    {
        private T m_State;
        public State(T State) => m_State = State;

        public static State<T2> Bind<T2>(State<T> State, Func<T, State<T2>> Func) =>
            Func(State.m_State);

        public static State<T2> Bind<T2>(State<T> State, Func<State<T2>> Func) =>
            Bind(State, Function.Pipe(Functions.Identity<T>(), Func));

        public static State<T2> Map<T2>(State<T> State, Func<T, T2> Func) =>
            Bind(State, P => new State<T2>(Func(P)));

        public State<T2> Bind<T2>(Func<T, State<T2>> Func) =>
            Bind(this, Func);

        public State<T2> Bind<T2>(Func<State<T2>> Func) =>
            Bind(this, Func);

        public State<T2> Map<T2>(Func<T, T2> Func) =>
            Map(this, Func);
    }
}
