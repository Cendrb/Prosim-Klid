using Klid.Shards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Klid
{
    [Serializable]
    class ShardSlot : ISerializable
    {
        private static readonly string SHARD_KEY = "shard";
        private static readonly string SHARDS_DESCRIPTION_KEY = "description";
        private static readonly string ALLOWED_SHARDS_KEY = "allowed";

        public string AllowedShardsDescription { get; private set; }
        public Shard Shard { get; private set; }

        public event Func<Shard, Boolean> ShardSet = delegate { return false; };

        private List<Type> allowedShardTypes;

        public ShardSlot(string alowedShardsDescription, params Type[] types)
        {
            allowedShardTypes = new List<Type>();
            AllowedShardsDescription = alowedShardsDescription;
            foreach (Type type in types)
                addShardType(type);
        }

        protected ShardSlot(SerializationInfo info, StreamingContext context)
        {
            Shard = (Shard)info.GetValue(SHARD_KEY, typeof(Shard));
            AllowedShardsDescription = info.GetString(SHARDS_DESCRIPTION_KEY);
            allowedShardTypes = (List<Type>)info.GetValue(ALLOWED_SHARDS_KEY, typeof(List<Type>));
        }

        private void addShardType(Type type)
        {
            if (typeof(Shard).IsAssignableFrom(type))
            {
                allowedShardTypes.Add(type);
            }
            else
                throw new ArgumentException("Only types which inherit from Shard class are supported");
        }

        public bool SetShard(Shard shard)
        {
            if (IsAllowed(shard) && ShardSet(shard))
            {
                Shard = shard;
                return true;
            }
            else
                return false;

        }

        public bool IsAllowed(Shard shard)
        {
            foreach (Type type in allowedShardTypes)
                if (type == shard.GetType())
                    return true;
            return false;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(SHARD_KEY, Shard);
            info.AddValue(SHARDS_DESCRIPTION_KEY, AllowedShardsDescription);
            info.AddValue(ALLOWED_SHARDS_KEY, allowedShardTypes);
        }
    }
}
