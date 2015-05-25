using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Klid.Blocks;

namespace Klid
{
    public class DataStorage : MonoBehaviour
    {
        public static CraftedBlocksStorage CraftedBlocks { get; private set; }

        static DataStorage()
        {
            CraftedBlocks = new CraftedBlocksStorage(Application.persistentDataPath + "/crafted_blocks.dat");
        }
    }
}
