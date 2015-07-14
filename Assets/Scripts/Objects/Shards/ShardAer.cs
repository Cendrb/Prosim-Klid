using Klid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Klid.Shards
{
    class ShardAer : Shard
    {
        public ShardAer()
            : base("Aer", Color.yellow)
        {

        }

        protected override void getAffectedProperties(List<AffectedPropertyValuePair> affectedProperties)
        {
            affectedProperties.Add(new AffectedPropertyValuePair(typeof(PropertyAngularDrag), -10));
            affectedProperties.Add(new AffectedPropertyValuePair(typeof(PropertyMass), -1));
            affectedProperties.Add(new AffectedPropertyValuePair(typeof(PropertyHealth), -10));
        }
    }
}
