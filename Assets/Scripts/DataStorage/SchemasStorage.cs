using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Klid
{
    class SchemasStorage : ISaveable
    {
        string path;

        public List<Schema> Schemas { get; private set; }

        public SchemasStorage(string path)
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
                Schemas = (List<Schema>)formatter.Deserialize(stream);
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
                formatter.Serialize(stream, Schemas);
            }
        }

        private void saveDefaults()
        {
            Schemas = new List<Schema>();
            Save();
        }
    }
}
