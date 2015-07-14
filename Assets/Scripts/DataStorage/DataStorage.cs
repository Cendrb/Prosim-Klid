using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Klid.Blocks;
using Klid.Shards;

namespace Klid
{
    class DataStorage : MonoBehaviour
    {
        // dynamically loaded from files
        public static CraftedBlocksStorage CraftedBlocks { get; private set; }
        public static SchemasStorage Schemas { get; private set; }
        public static ShardsStorage Shards { get; private set; }

        // statically set
        public static List<BlockTemplate> CraftibleBlocks { get; private set; }
        public static List<Tab> CraftingTabs { get; private set; }
        public static List<Tab> BuildingTabs { get; private set; }

        static DataStorage()
        {
            Debug.Log(Application.persistentDataPath);
            CraftedBlocks = new CraftedBlocksStorage(Application.persistentDataPath + "/crafted_blocks.dat");
            CraftedBlocks.Load();
            Schemas = new SchemasStorage(Application.persistentDataPath + "/schemas.dat");
            Schemas.Load();
            Schemas.Schemas.Add(new Schema(16, 12));
            Shards = new ShardsStorage(Application.persistentDataPath + "/shards.dat");
            Shards.Load();
             
            CraftingTabs = new List<Tab>();
            CraftingTabs.Add(new Tab("Building blocks"));
            CraftingTabs.Add(new Tab("Basic mechanics"));
            CraftingTabs.Add(new Tab("Shards"));

            BuildingTabs = new List<Tab>();
            BuildingTabs.Add(new Tab("Building blocks"));
            BuildingTabs.Add(new Tab("Basic mechanics"));

            CraftibleBlocks = new List<BlockTemplate>();
            CraftibleBlocks.Add(new BlockTemplate("Brick", "Basic building block", typeof(BlockBrick), CraftingTabs[0]));


            CraftedBlocks.CraftedBlocks.Add(CraftibleBlocks[0].GetBlockInstance());
            CraftedBlocks.CraftedBlocks[0].ShardSlots[0].SetShard(new ShardHealth());
        }
    }
}
