using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Properties
{
    class AffectedPropertyValuePair
    {
        public Type Property { get; private set; }
        public float ValueChange { get; private set; }

        public AffectedPropertyValuePair(Type property, float valueChange)
        {
            Property = property;
            ValueChange = valueChange;
        }

        public void ApplyChange(Property property)
        {
            if(property.GetType() == Property)
            {
                property.Value += ValueChange;
            }
        }
    }
}
