using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serenity.Modules
{
    internal interface IModule
    {
        void HandleCommand(IEnumerable<string> args);
    }
}
