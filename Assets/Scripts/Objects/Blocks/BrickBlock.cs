using Klid.Blocks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klid.Blocks
{
    class BrickBlock : Block
    {
        public BrickBlock(string name, string description)
            : base(name, description)
        {

        }

        protected List<ShardSlot> getShardSlots()
        {
            List<ShardSlot> shardSlots = new List<ShardSlot>();
            ADD SOME FUCKING SHARD TYPES HERE
            return shardSlots;
        }

        protected BlockGameInterface getGameInterface()
        {
            return new BrickBlockGameInterface();
        }
    }
}
