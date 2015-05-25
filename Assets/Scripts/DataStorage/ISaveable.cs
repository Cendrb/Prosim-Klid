using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid
{
    interface ISaveable
    {
        public void Load();
        public void Save();
    }
}
