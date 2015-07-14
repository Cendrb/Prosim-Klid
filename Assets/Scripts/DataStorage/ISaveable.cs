using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid
{
    interface ISaveable
    {
        void Load();
        void Save();
    }
}
