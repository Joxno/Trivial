using System;

namespace Trivial.Helpful.Func;

public static class FunctionMaybe
{
    public static Func<Maybe<T>, bool> MaybeHasValue<T>() =>
        (Maybe) => Maybe.HasValue;
}
