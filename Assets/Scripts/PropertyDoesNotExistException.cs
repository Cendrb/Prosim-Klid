using Klid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid
{
    class PropertyDoesNotExistException : Exception
    {
        public PropertyDoesNotExistException(Type property)
            : base(String.Format("Property {0} does not exist", property.Name))
        {

        }
    }
}
