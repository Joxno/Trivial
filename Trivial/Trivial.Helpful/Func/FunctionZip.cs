using System.Collections.Generic;
using System.Linq;

namespace Trivial.Helpful.Func;

public static class FunctionZip
{
    public static IEnumerable<(T1 First, T2 Second)> Zip<T1, T2>(IEnumerable<T1> First, IEnumerable<T2> Second) =>
        First.Zip(Second);
}
