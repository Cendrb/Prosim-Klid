using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Klid.Properties
{
    abstract class Property
    {
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }
        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!allowsNegatives && _value < 0)
                    throw new PropertyCannotBeNegativeException();
                _value = value;
            }
        }
        private bool allowsNegatives;
        private float defaultValue;

        public Property(string name, float defaultValue, bool allowsNegatives)
        {
            Name = name;
            Value = defaultValue;
            Sprite = Resources.Load<Sprite>("Graphics/Properties/" + Name);
            this.defaultValue = defaultValue;
            this.allowsNegatives = allowsNegatives;
        }

        public void Reset()
        {
            Value = defaultValue;
        }
    }
}
