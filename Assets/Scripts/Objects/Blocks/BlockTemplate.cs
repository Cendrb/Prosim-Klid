using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Klid.Blocks
{
    class BlockTemplate
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Sprite { get; private set; }
        public Type BlockClass { get; private set; }

        public BlockTemplate(string name, string description, Type blockClass)
        {
            Name = name;
            Description = description;
            Sprite = Resources.Load<Sprite>("Graphics/Blocks/" + Name);
            BlockClass = blockClass;
        }

        public Block GetBlockInstance()
        {
            return (Block)Activator.CreateInstance(BlockClass, Name, Description);
        }
    }
}
