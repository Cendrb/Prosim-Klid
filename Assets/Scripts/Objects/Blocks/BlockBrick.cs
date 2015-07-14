using Klid.Blocks.Interfaces;
using Klid.Properties;
using Klid.Shards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Blocks
{
    class BlockBrick : Block
    {
        public BlockBrick(string name, string description)
            : base(name, description, DataStorage.BuildingTabs[0])
        {

        }

        protected override void getShardSlots(List<ShardSlot> shardSlots)
        {
            shardSlots.Add(new ShardSlot("Healthy penises", typeof(ShardHealth)));
        }

        protected override BlockGameInterface getGameInterface()
        {
            return new BlockGameInterfaceBrick(Properties);
        }

        protected override void getProperties(List<Property> properties)
        {
            properties.Add(new PropertyHealth());
            properties.Add(new PropertyMass());
        }
    }
}
