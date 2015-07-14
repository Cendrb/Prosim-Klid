using Klid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Klid.Shards
{
    class ShardHealth : Shard
    {
        public ShardHealth()
            : base("Health", Color.red)
        {

        }

        protected override void getAffectedProperties(List<AffectedPropertyValuePair> affectedProperties)
        {
            affectedProperties.Add(new AffectedPropertyValuePair(typeof(PropertyHealth), 10));
        }
    }
}
