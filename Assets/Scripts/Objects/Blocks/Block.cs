using Klid.Blocks.Interfaces;
using Klid.Properties;
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
        public Tab Tab { get; private set; }

        public List<ShardSlot> ShardSlots { get; private set; }

        public Sprite Sprite { get; private set; }

        public BlockGameInterface GameInterface { get; private set; }

        public List<Property> Properties { get; private set; } 

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

        public Block(string name, string description, Tab tab)
        {
            Name = name;
            Description = description;
            Id = GetNextAvailableId();
            Sprite = Resources.Load<Sprite>("Graphics/Blocks/" + Name);
            Tab = tab;

            // get properties from children
            Properties = new List<Property>();
            getProperties(Properties);

            GameInterface = getGameInterface();

            ShardSlots = new List<ShardSlot>();
            getShardSlots(ShardSlots);
            bindEventListeners();

            applyShardsToProperties();
        }

        protected Block(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString(NAME_KEY);
            Description = info.GetString(DESCRIPTION_KEY);
            Id = info.GetInt32(ID_KEY);
            Sprite = Resources.Load<Sprite>("Graphics/Blocks/" + Name);

			// get properties from children
            Properties = new List<Property>();
			getProperties(Properties);

            GameInterface = getGameInterface();

            ShardSlots = (List<ShardSlot>)info.GetValue(SHARD_SLOTS_KEY, typeof(List<ShardSlot>));
            bindEventListeners();
		

            applyShardsToProperties();
        }

        private void bindEventListeners()
        {
            foreach (ShardSlot slot in ShardSlots)
                slot.ShardSet += shardSet;
        }

        private bool shardSet(Shard shard)
        {
            Debug.Log("Shard set: " + shard.GetType().ToString());
            return applyShardsToProperties();
        }

        private bool applyShardsToProperties()
        {
            try
            {
                foreach (Property property in Properties)
                {
                    property.Reset();
                    foreach (ShardSlot slot in ShardSlots)
                        if (slot.Shard != null)
                            slot.Shard.ApplyShardEffects(property);
                }

                GameInterface.reloadProperties();

                return true;
            }
            catch(PropertyCannotBeNegativeException)
            {
                return false;
            }
        }

        protected abstract void getShardSlots(List<ShardSlot> slots);
        protected abstract void getProperties(List<Property> properties);
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
