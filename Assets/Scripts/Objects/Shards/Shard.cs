using Klid.Blocks.Interfaces;
using Klid.Properties;
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
        public List<AffectedPropertyValuePair> AffectedProperties { get; private set; }
        public Sprite Sprite { get; private set; }

        public Shard(string name, Color color)
        {
            Name = name;
            MainColor = color;
            AffectedProperties = new List<AffectedPropertyValuePair>();
            Sprite = Resources.Load<Sprite>("Graphics/shard");
        }

        public void ApplyShardEffects(Property property)
        {
            AffectedPropertyValuePair pair = (from aPVP in AffectedProperties where (aPVP.Property == property.GetType()) select aPVP).First();
            if (pair != null)
                pair.ApplyChange(property);
        }

        protected abstract void getAffectedProperties(List<AffectedPropertyValuePair> affectedProperties);
    }
}
