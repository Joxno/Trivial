using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.Helpful.Func
{
    public static class FunctionMaybe
    {
        public static Func<Maybe<T>, bool> MaybeHasValue<T>() =>
            (Maybe) => Maybe.HasValue;
    }
}
