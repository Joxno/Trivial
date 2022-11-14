using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Trivial.Helpful.Def;
using static Trivial.Helpful.Function;
using static Trivial.Helpful.Functions;

namespace Trivial.Helpful
{
    public class State<TS> : State<TS, Unit>
    {
        public State(TS State) : base(State, Defaults.Unit) { }
    }

    public class State<TS, TV>
    {
        private TV m_Value;
        private TS m_State;
        public State(TS State, TV Value) => 
            (m_State, m_Value) = (State, Value);

        public static State<T1, T2> Bind<T1, T2>(State<TS, TV> State, Func<TS, State<T1, T2>> Func) =>
            Func(State.m_State);

        public static State<T1, T2> Bind<T1, T2>(State<TS, TV> State, Func<TS, TV, State<T1, T2>> Func) =>
            Func(State.m_State, State.m_Value);

        public static State<T1, T2> Bind<T1, T2>(State<TS, TV> State, Func<State<T1, T2>> Func) =>
            Bind(State, Pipe(Identity<TS>(), Func));

        public static State<T1, T2> Map<T1, T2>(State<TS, TV> State, Func<TS, TV, (T1, T2)> Func) =>
            Bind(State, (PS, PV) => Let(Func(PS, PV), SV => new State<T1, T2>(SV.Item1, SV.Item2))());

        public State<T1, T2> Bind<T1, T2>(Func<TS, State<T1, T2>> Func) =>
            Bind(this, Func);

        public State<T1, T2> Bind<T1, T2>(Func<State<T1, T2>> Func) =>
            Bind(this, Func);

        public State<T1, T2> Map<T1, T2>(Func<TS, TV, (T1, T2)> Func) =>
            Map(this, Func);

        public State<T, Unit> Map<T>(Func<TS, TV, T> Func) =>
            Map(this, (TS S, TV V) => (Func(S, V), Defaults.Unit));

        public State<T, TV> Map<T>(Func<TS, T> Func) =>
            Map(this, (TS S, TV V) => (Func(S), V));

        public State<T, T2> Map<T, T2>(Func<TS, (T, T2)> Func) =>
            Map(this, (TS S, TV V) => Let(Func(S), T => (T.Item1, T.Item2))());

        public (TS, TV) StateValue => (m_State, m_Value);
    }
}
