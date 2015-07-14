using Klid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Blocks.Interfaces
{
    class BlockGameInterfaceBrick : BlockGameInterface
    {
        public BlockGameInterfaceBrick(List<Property> properties)
             : base("Brick", properties)
        {

        }

        public override void reloadProperties()
        {
            base.reloadProperties();
        }
    }
}
