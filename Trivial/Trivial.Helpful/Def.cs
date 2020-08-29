using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.Helpful
{
    public static class Def
    {
        public static Func<T> Fn<T>(T Value) => () => Value;
        public static Func<T> Fn<T>(Func<T> F) => () => F();

        public static void Let<T>(T Val, Action<T> Scope) { Scope(Val); }
        public static void Let<T1, T2>(T1 V1, T2 V2, Action<T1, T2> Scope) { Scope(V1, V2); }
        public static void Let<T1, T2, T3>(T1 V1, T2 V2, T3 V3, Action<T1, T2, T3> Scope) { Scope(V1, V2, V3); }
        public static void Let<T1, T2, T3, T4>(T1 V1, T2 V2, T3 V3, T4 V4, Action<T1, T2, T3, T4> Scope) { Scope(V1, V2, V3, V4); }
    }
}
