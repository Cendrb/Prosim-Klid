using Klid.Blocks.Interfaces;
using Klid.Shards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

namespace Klid.Blocks
{
    [Serializable]
    abstract class Block : ISerializable
    {
        private static readonly string NAME_KEY = "name";
        private static readonly string DESCRIPTION_KEY = "description";
        private static readonly string ID_KEY = "id";
        private static readonly string SHARD_SLOTS_KEY = "shard_slots";

        private static readonly string NEXT_ID_KEY = "next_id";
        private static int nextId;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Id { get; private set; }

        public List<ShardSlot> ShardSlots { get; private set; }

        public Sprite Sprite { get; private set; }

        public BlockGameInterface GameInterface { get; private set; }

        static Block()
        {
            nextId = PlayerPrefs.GetInt(NEXT_ID_KEY);
        }

        
        public int GetNextAvailableId()
        {
            int returnable = nextId;
            nextId++;
            PlayerPrefs.SetInt(NEXT_ID_KEY, nextId);
            PlayerPrefs.Save();
            return returnable;
        }

        public Block(string name, string description)
        {
            Name = name;
            Description = description;
            Id = GetNextAvailableId();
            Sprite = Resources.Load<Sprite>("Graphics/Blocks/" + Name);

            ShardSlots = getShardSlots();
            bindEventListeners();

            ResetGameInterfaceAndApplyShards();
        }

        protected Block(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString(NAME_KEY);
            Description = info.GetString(DESCRIPTION_KEY);
            Id = info.GetInt32(ID_KEY);
            ShardSlots = (List<ShardSlot>)info.GetValue(SHARD_SLOTS_KEY, typeof(List<ShardSlot>));
            bindEventListeners();

            ResetGameInterfaceAndApplyShards();
        }

        private void bindEventListeners()
        {
            foreach (ShardSlot slot in data.ShardSlots)
                slot.ShardSet += shardSet;
        }

        private void shardSet(Shard shard)
        {
            Debug.Log("Shard set: " + shard.GetType().ToString());
            ResetGameInterfaceAndApplyShards();
        }

        public void ResetGameInterfaceAndApplyShards()
        {
            GameInterface = getGameInterface();
            GameInterface.ApplyShards(data.ShardSlots);
        }

        protected abstract List<ShardSlot> getShardSlots();
        protected abstract BlockGameInterface getGameInterface();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(NAME_KEY, Name);
            info.AddValue(DESCRIPTION_KEY, Description);
            info.AddValue(SHARD_SLOTS_KEY, ShardSlots);
            info.AddValue(ID_KEY, Id);
        }
    }
}
