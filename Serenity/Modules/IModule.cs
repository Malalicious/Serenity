using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serenity.Objects;

namespace Serenity.Modules
{
    public interface IModule
    {
        void HandleCommand(IEnumerable<string> args);
        Fov GetFov();
    }
}
