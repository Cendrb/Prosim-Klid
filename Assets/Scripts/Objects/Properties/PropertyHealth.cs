using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Properties
{
    class PropertyHealth : Property
    {
        public PropertyHealth()
            : base("Health", 20, false)
        {

        }

        public PropertyHealth(float defaultValue)
            : base("Health", defaultValue, false)
        {

        }
    }
}
