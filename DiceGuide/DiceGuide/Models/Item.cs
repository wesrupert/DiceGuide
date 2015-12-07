using System;
using System.Diagnostics;
using System.Xml;

namespace DiceGuide.Models
{
    class Item : SerializableReference
    {
        protected string _name = string.Empty;
        protected string _type = string.Empty;
        protected uint? _weight = null;
        protected string _text = string.Empty;

        /// <summary>The item's name.</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>The item's type.</summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        /// <summary>The item's weight.</summary>
        public uint? Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged("Weight"); }
        }

        /// <summary>The item's description.</summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }

        /// <summary>Creates an empty item.</summary>
        public Item() { }

        /// <summary>
        /// Creates a new item from import data.
        /// </summary>
        /// <param name="node">The XmlNode containing the item data.</param>
        public Item(XmlNode node)
        {
            Debug.Assert(node.Name == "item", "Initializing item with wrong node type!");
            Debug.Assert(node.HasChildNodes, "Initializing item with empty node!");
            if (node.Name != "item" || !node.HasChildNodes)
                return;

            _name = GetStringFromNode(node, "name");
            _type = GetStringFromNode(node, "type");
            _weight = GetUnsignedFromNode(node, "weight");
            _text = GetStringFromNode(node, "text", "\n");
        }

        public override string ToString() => $"{Name}: {Type} item, {Weight ?? 0} lbs.\n{Text}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
