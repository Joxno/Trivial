using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.Functional.Experimental
{
    public interface HigherKind<T>
    {
    }

    public interface Kind<T, T2> : HigherKind<Kind<T, T2>>
    {
        public T Value { get; }
    }

    public interface Functor<T>
    {
        //Kind<T2, HKT> fmap<T2, HKT>(Func<T, T2> Func, Kind<T, HKT> fa);
        Kind<T2, HKT> fmap<T2, HKT>(Func<T, T2> Func);
    }

    public class MockMaybe { }

    public class MockMaybe<T> : MockMaybe, Kind<T, MockMaybe<T>>, Functor<T>
    {
        private T m_Value;
        public T Value => m_Value;

        public MockMaybe(T Value) => m_Value = Value;

        public Kind<T2, HKT> fmap<T2, HKT>(Func<T, T2> Func)
        {
            return (Kind<T2, HKT>)new MockMaybe<T2>(Func(Value));
        }
    }

    public static class HelperFunctions
    {
        public static T ToType<T, HKT>(this Kind<T, HKT> Kind) => Kind.Value;
    }



    public record Person(string Name, int Age);

    public static class HKT
    {
        public static Func<Person, Kind<int, MockMaybe<int>>> Age() =>
            (Person P) => new MockMaybe<int>(P.Age);

        public static TR GetAgeOfPerson<T, TR>(T Value, Func<T, Kind<Person, TR>> Func) =>
            (TR)Func(Value);

        public static void Test()
        {
            var t_MTest = GetAgeOfPerson("User", (string s) => new MockMaybe<Person>(new Person("Steve", 30)));
        }

        public static Functor<Person> GetPerson() => new MockMaybe<Person>(new Person("TestDummy", 28));

        public static void MapOverPerson()
        {
            var t_Functor = GetPerson();
            t_Functor.fmap<int, MockMaybe>((Person P) => P.Age);
        }
    }
}
