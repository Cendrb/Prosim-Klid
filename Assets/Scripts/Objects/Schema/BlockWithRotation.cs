using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Klid.Schema
{
    [Serializable]
    class BlockWithRotation : ISerializable
    {
        private static readonly string ID_KEY = "id";
        private static readonly string ROTATION_KEY = "rotation";

        public Block Block { get; private set; }
        public int Rotation { get; private set; }

        public BlockWithRotation(Block block, int rotation = 0)
        {
            Block = block;
            Rotation = rotation;
        }

        protected BlockWithRotation(SerializationInfo info, StreamingContext context)
        {
            Block = DataStorage.CraftedBlocks.FindById(info.GetInt32(ID_KEY));
            Rotation = info.GetInt32(ROTATION_KEY)¨;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ID_KEY, Block.Id);
            info.AddValue(ROTATION_KEY, Rotation);
        }
    }
}
