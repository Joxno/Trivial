using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Trivial.Helpful.Def;

namespace Trivial.Helpful
{
    public interface IEffect
    {
    }

    public interface IEffect<T> : IEffect
    {
        Func<T> Effect { get; init; }
        T Execute() => Effect.Invoke();
    }

    public static class Effect
    {
        public static Effect<T, TR> Create<T, TR>(T Effect) where T : IEffect<TR> =>
            Effect<T, TR>.Create(Effect);
    }

    public class Effect<T, TR> 
        where T : IEffect<TR>
    {
        private Func<TR> m_EffectFunc;
        private ImmutableList<IEffect> m_Effects;

        private Effect(ImmutableList<IEffect> Effects, Func<TR> Effect) =>
            (m_Effects, m_EffectFunc) = (Effects, Effect);

        public static Effect<T, TR> Create(IEffect<TR> Effect) =>
            new Effect<T, TR>(ImmutableList.Create((IEffect)Effect), Effect.Effect);

        /* >> */
        public static Effect<T1, TR1> Bind<T1, TR1>(Effect<T, TR> Value, Func<T, Effect<T1, TR1>> Func)
            where T1 : IEffect<TR1> =>
                Let(Func(), M => 
                    new Effect<T1, TR1>(
                        Value.m_Effects.Concat(M.m_Effects).ToImmutableList(), 
                        () => { Value.m_EffectFunc.Invoke(); return M.m_EffectFunc.Invoke(); }))();

        public static Effect<T1, TR1> Map<T1, TR1>(Effect<T, TR> Value, Func<TR, TR1> Func)
            where T1 : IEffect<TR1> =>
            Bind(Value, () => Effect.Create(Func));

        public Effect<T2> Bind<T2>(Func<Effect<T2>> Func) where T2 : IEffect =>
            Bind(this, Func);

        public Effect<T2> Map<T2>(T2 Func) where T2 : IEffect =>
            Map(this, Func);

        public void Perform()
        {
            //m_Effects
            //    .Select(E => E.Effect)
            //    .Reverse()
            //    .Aggregate((Result, Item) => () => { Result?.Invoke(); Item?.Invoke(); })
            //    .Invoke();
        }

        public ImmutableList<IEffect> Effects => m_Effects;
    }
}
