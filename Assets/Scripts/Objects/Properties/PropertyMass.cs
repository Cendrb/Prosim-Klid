using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Properties
{
    class PropertyMass : Property
    {
        public PropertyMass()
            : base("Mass", 1, false)
        {

        }

        public PropertyMass(float defaultValue)
            : base("Mass", defaultValue, false)
        {

        }
    }
}
