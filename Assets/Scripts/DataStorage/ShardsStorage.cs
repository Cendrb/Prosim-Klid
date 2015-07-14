using Klid.Shards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Klid
{
    class ShardsStorage : ISaveable
    {
        string path;

        public List<Shard> Shards { get; private set; }

        public ShardsStorage(string path)
        {
            this.path = path;
        }

        public void Load()
        {
            if (!File.Exists(path))
                saveDefaults();
            FileStream stream = File.Open(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Shards = (List<Shard>)formatter.Deserialize(stream);
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
                formatter.Serialize(stream, Shards);
            }
        }

        private void saveDefaults()
        {
            Shards = new List<Shard>();
            Save();
        }
    }
}
