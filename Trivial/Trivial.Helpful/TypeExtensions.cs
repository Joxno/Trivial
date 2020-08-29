using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.Helpful
{
    public static class TypeExtensions
    {
        public static object EraseType<T>(this T Value) => Value;

        public static (object, Type) StripType<T>(this T Value) => (Value, typeof(T));
    }
}
