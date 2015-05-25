using Klid.Blocks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Klid.Shards
{
    abstract class Shard
    {
        public string Name { get; private set; }
        public Color MainColor { get; private set; }

        public Shard(string name, Color color)
        {
            Name = name;
            MainColor = color;
        }

        public void ApplyShardEffects(BlockGameInterface gameInterface)
        {
            Debug.Log(String.Format("Applying shard {0} to game block interface {1}", Name, gameInterface.GetType().ToString()));
            applyShardEffects(gameInterface);
        }

        protected abstract void applyShardEffects(BlockGameInterface gameInterface);
    }
}
