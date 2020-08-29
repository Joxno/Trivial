using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;
using static Trivial.Helpful.Functions;

namespace Trivial.Helpful.Experimental
{
    public class VariantList
    {
        private Cons<object, object> m_InternalList = Functions.Cons(Null.EraseType(), Null.EraseType());

        public VariantList(params object[] ValueList)
        {
            if (ValueList.Length == 1)
                m_InternalList = Functions.Cons(ValueList[0], Null.EraseType());
            else if (ValueList.Length > 1)
            {
                var t_List = Return(ValueList[0]);
                for (int i = 1; i < ValueList.Length; i++)
                    t_List = t_List.Bind(_ => Return(ValueList[i]));

                m_InternalList = t_List.m_InternalList;
            }
        }

        public VariantList(object Value) => m_InternalList = Functions.Cons(Value, Null.EraseType());
        public VariantList(Cons<object, object> List) => m_InternalList = List;

        public static VariantList Bind(VariantList L, Func<object, VariantList> Func) =>
            Functions.Cons(Func(L.m_InternalList.Left).m_InternalList.Left, L.m_InternalList.EraseType());

        public static VariantList Bind(VariantList L, Func<VariantList> Func) =>
            Bind(L, Function.Pipe(Helpful.Functions.Identity<object>(), () => Func()));

        public VariantList Bind(Func<object, VariantList> Func) =>
            Bind(this, Func);

        public VariantList Bind(Func<VariantList> Func) =>
            Bind(this, Func);

        public static VariantList Return<T>(T Value) =>
            new VariantList(Functions.Cons(Value.EraseType(), Null.EraseType()));

        public static VariantList Return(params object[] Values) =>
            new VariantList(Values);

        public VariantList Map(Func<object, object> Func) =>
            Bind(this, (P) => Return(Func(P)));

        public class VariantListNullType { }
        public static VariantListNullType Null => new VariantListNullType();

        public static implicit operator VariantList(Cons<object, object> List)
            => new VariantList(List);

        public VariantList Add<T>(T Value) =>
            Bind(this, () => Return(Value));
        public VariantList Add(object Value) =>
            Add(Value);
    }

    public static class VariantListPlayground
    {
        public static void Test()
        {
            var t_List = Cons(1, Cons("", 2));

            var (left, right) = t_List;

            var t_LargerList = Cons(1, Cons(2, Cons(3, Cons(4, 5))));
            var (first, (second, rest)) = t_LargerList;
        }

        public static void TestList()
        {
            var t_List =
                VariantList
                .Return(1)
                .Bind(_ => VariantList.Return(2))
                .Bind(_ => VariantList.Return(""))
                .Bind(_ => VariantList.Return(1.3f));

            t_List.Map(o => o switch
            {
                float f => f + 1.5f,
                int i => i * 2,
                _ => o
            });

            var t_NewList = VariantList.Return(1, 2, 3, "", null, "", 1.5f);
            t_NewList.Map(o => o switch
            {
                string s => string.IsNullOrEmpty(s) ? 0 : int.Parse(s),
                float f => (int)f,
                int i => i * 2,
                null => 0,
                _ => o
            });

            var t_ShorterSyntaxList =
                VariantList
                .Return(1)
                .Add(10)
                .Add("")
                .Add(1.6f)
                .Add(null);
        }
    }
}
