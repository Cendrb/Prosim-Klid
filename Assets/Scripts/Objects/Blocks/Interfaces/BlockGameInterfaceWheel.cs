using Klid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Blocks.Interfaces
{
    class BlockGameInterfaceWheel : BlockGameInterface
    {
        public float AngularDrag { get; private set; }

        public BlockGameInterfaceWheel(List<Property> properties)
            : base("Wheel", properties)
        {
            
        }

        public override void reloadProperties()
        {
            base.reloadProperties();
            AngularDrag = getPropertyValue(typeof(PropertyAngularDrag));
        }
    }
}
