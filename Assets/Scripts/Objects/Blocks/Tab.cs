using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Klid.Blocks
{
    class Tab
    {
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }

        public Tab(string name)
        {
            Name = name;
            Sprite = Resources.Load<Sprite>("Graphics/Tabs/" + Name);
        }
    }
}
