using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Functional;

namespace Trivial.Tests
{
    [TestFixture]
    public class TryInvokeTests
    {
        [Test]
        public void TryInvokePersonManipulation()
        {
            Func<IEnumerable<MockPerson>> t_GetPeople = () => MockPeople;
            Func<int, IEnumerable<MockPerson>, IEnumerable<MockPerson>> t_GetPeopleAboveAge =
                (Age, People) => People.Where(P => P.Age > Age);

            Func<MockPerson, MockPerson> t_IncrementAge = P => { P.Age += 1; return P; };
            Func<int, IEnumerable<MockPerson>, IEnumerable<MockPerson>> t_GetPersonById = 
                (Id, People) => People.Where(P => P.Id == Id);

            var t_Invocation =
                new TryInvoke<IEnumerable<MockPerson>>(t_GetPeople)
                .Map(t_GetPeopleAboveAge.Apply(24))
                .Map(t_GetPersonById.Apply(2))
                .Map(L => L.Select(t_IncrementAge).FirstOrDefault());

            var t_Result = t_Invocation.Run();
            t_Result.HasError.Should().BeFalse();
            t_Result.Value.Id.Should().Be(2);
            t_Result.Value.Age.Should().Be(29);
        }

        private class MockPerson
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

        }

        private List<MockPerson> MockPeople = new()
        {
            new() { Id = 0, Name = "Foobar", Age = 20 },
            new() { Id = 1, Name = "Foo", Age = 25 },
            new() { Id = 2, Name = "Bar", Age = 28 },
        };
    }
}
