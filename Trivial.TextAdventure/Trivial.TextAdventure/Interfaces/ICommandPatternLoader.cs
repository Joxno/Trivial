using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Helpful;
using Trivial.TextAdventure.Data;

namespace Trivial.TextAdventure.Interfaces
{
    public interface ICommandPatternLoader
    {
        public Func<JSON, Maybe<(ImmutableList<CommandPattern>, ImmutableList<Pattern>)>> LoadFromJson { get; }
        public Func<FilePath, Maybe<(ImmutableList<CommandPattern>, ImmutableList<Pattern>)>> LoadFromFile { get; }
    }
}
