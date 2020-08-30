using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.TextAdventure.DTO
{
    public class CommandPatternDTO
    {
        public string Command { get; set; }
        public PatternDTO[] Pattern { get; set; }
    }
}
