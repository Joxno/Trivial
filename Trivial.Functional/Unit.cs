namespace Trivial.Functional;

public record struct Unit;

public static partial class Defaults
{
    public static Unit Unit => new Unit();
}
