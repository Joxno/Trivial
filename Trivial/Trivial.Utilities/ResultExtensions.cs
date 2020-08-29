using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;

namespace Trivial.Utilities
{
    public static class ResultExtensions
    {
        public static void Then<T>(this Result<T> E, Action<T> ThenMethod)
        {
            if (E.HasValue)
                ThenMethod?.Invoke(E.Value);
        }

        public static Result<T2> Then<T1, T2>(this Result<T1> E, Func<T1, T2> ThenMethod)
        {
            if (E.HasValue)
                return Try.Invoke(ThenMethod, E.Value);

            return new Result<T2>(E.Error);
        }
    }
}
