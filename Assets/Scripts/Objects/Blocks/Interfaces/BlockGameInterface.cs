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
        public string Name { get; private set; }

        public BlockGameInterface(string name, float health)
        {
            Name = name;
            Health = health;
        }

        public void ApplyShards(List<ShardSlot> slots)
        {
            foreach (ShardSlot slot in slots)
                slot.Shard.ApplyShardEffects(this);
        }
    }
}
