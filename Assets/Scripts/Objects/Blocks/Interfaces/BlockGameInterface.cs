using Klid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Klid.Blocks.Interfaces
{
    abstract class BlockGameInterface : MonoBehaviour
    {
        public float Health { get; private set; }
        public float Mass { get; private set; }

        public string Name { get; private set; }
        protected List<Property> Properties { get; private set; }

        public BlockGameInterface(string name, List<Property> properties)
        {
            Name = name;
            Properties = properties;
        }

        public virtual void reloadProperties()
        {
            Health = getPropertyValue(typeof(PropertyHealth));
            Mass = getPropertyValue(typeof(PropertyMass));
        }

        protected float getPropertyValue(Type propertyType)
        {
            foreach (Property property in Properties)
                if (propertyType.IsInstanceOfType(property))
                    return property.Value;
            throw new PropertyDoesNotExistException(propertyType);
        }
    }
}
