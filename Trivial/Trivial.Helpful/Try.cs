using System;

namespace Trivial.Helpful;

public static class Try
{
    public static Maybe<T> From<T>(Func<T> Func)
    {
        try
        {
            return new Maybe<T>(Func());
        }
        catch
        {
            return new Maybe<T>();
        }
    }

    public static Func<T, Maybe<T2>> GetFrom<T, T2>(Func<T, T2> Func) =>
        (P) =>
        {
            try
            {
                return new Maybe<T2>(Func(P));
            }
            catch
            {
                return new Maybe<T2>();
            }
        };

    public static Func<T, Result<Maybe<T2>>> GetInvokeFrom<T, T2>(Func<T, T2> Func) =>
        (P) =>
        {
            try
            {
                return new Result<Maybe<T2>>(new Maybe<T2>(Func(P)));
            }
            catch(Exception e)
            {
                return new Result<Maybe<T2>>(e);
            }
        };

    public static Result<T> Invoke<T>(Func<T> Func)
    {
        try
        {
            return new Result<T>(Func());
        }
        catch(Exception e)
        {
            return new Result<T>(e);
        }
    }

    public static Result<T2> Invoke<T, T2>(Func<T, T2> Func, T Val)
    {
        try
        {
            return new Result<T2>(Func(Val));
        }
        catch (Exception e)
        {
            return new Result<T2>(e);
        }
    }

    public static Func<T, Result<T2>> GetInvoke<T, T2>(Func<T, T2> Func) =>
        (P) =>
        {
            try
            {
                return new Result<T2>(Func(P));
            }
            catch (Exception e)
            {
                return new Result<T2>(e);
            }
        };

    public static Func<Result<T>> GetInvoke<T>(Func<T> Func) =>
        () =>
        {
            try
            {
                return new Result<T>(Func());
            }
            catch (Exception e)
            {
                return new Result<T>(e);
            }
        };

    public static Result<Unit> Invoke(Action Method)
    {
        try
        {
            Method?.Invoke();
            return new Result<Unit>(new Unit());
        }
        catch (Exception e)
        {
            return new Result<Unit>(e);
        }
    }
}
