using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversalServichs
{
    public interface IScriptService
    {
        bool Compile();

        void Run();
    }
}