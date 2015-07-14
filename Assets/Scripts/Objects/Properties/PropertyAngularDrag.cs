using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Properties
{
    class PropertyAngularDrag : Property
    {
        public PropertyAngularDrag()
            : base("Angular drag", 1, false)
        {

        }

        public PropertyAngularDrag(float defaultValue)
            : base("Angular drag", defaultValue, false)
        {

        }
    }
}
