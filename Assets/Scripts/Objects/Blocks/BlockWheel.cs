using Klid.Blocks.Interfaces;
using Klid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Blocks
{
    class BlockWheel : Block
    {
        public BlockWheel()
            : base("Wheel", "Does Rototo", DataStorage.BuildingTabs[1])
        {

        }

        protected override BlockGameInterface getGameInterface()
        {
            return new BlockGameInterfaceWheel(Properties);
        }

        protected override void getShardSlots(List<ShardSlot> slots)
        {
            
        }

        protected override void getProperties(List<Property> properties)
        {
            properties.Add(new PropertyHealth());
            properties.Add(new PropertyAngularDrag());
        }
    }
}
