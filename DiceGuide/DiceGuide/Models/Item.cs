using System;
using System.Diagnostics;
using System.Xml;

namespace DiceGuide.Models
{
    class Item : SerializableNotifier
    {
        /// <summary>The item's type.</summary>
        public string Type { get { return _type; } set { SetField(ref _type, value); } }
        protected string _type = string.Empty;

        /// <summary>The item's weight.</summary>
        public uint? Weight { get { return _weight; } set { SetField(ref _weight, value); } }
        protected uint? _weight = null;

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
            _description = GetStringFromNode(node, "text", "\n");
            _type = GetStringFromNode(node, "type");
            _weight = GetUnsignedFromNode(node, "weight");
        }

        public override string ToString() => $"{Name}: {Type} item, {Weight ?? 0} lbs.\n{Description}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
