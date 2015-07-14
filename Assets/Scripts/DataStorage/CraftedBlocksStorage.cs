using Klid.Blocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Klid
{
    class CraftedBlocksStorage : ISaveable
    {
        public List<Block> CraftedBlocks { get; private set; }

        string path;

        private int nextId;

        public CraftedBlocksStorage(string path)
        {
            this.path = path;
        }

        public Block FindById(int id)
        {
            return (from block in CraftedBlocks
                   where block.Id == id
                   select block).First();
        }

        public void Load()
        {
            if (!File.Exists(path))
                saveDefaults();
            FileStream stream = File.Open(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                CraftedBlocks = (List<Block>)formatter.Deserialize(stream);
                nextId = (int)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                stream.Close();
                saveDefaults();
            }
        }

        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, CraftedBlocks);
                formatter.Serialize(stream, nextId);
            }
        }

        private void saveDefaults()
        {
            CraftedBlocks = new List<Block>();
            Save();
        }
    }
}
