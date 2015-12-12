using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DiceGuide.Models
{
    class Item : SerializableNotifier
    {
        /// <summary>The XML key used to retrieve instances from XML files.</summary>
        private const string XmlKey = "item";

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
        public Item(XmlNode node) : base(node, XmlKey)
        {
            _type = GetStringFromNode(node, "type");
            _weight = GetUnsignedFromNode(node, "weight");
        }

        /// <summary>
        /// Gets the items from the children of the given node.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        /// <returns>List containing all items found in the node.</returns>
        internal static List<Item> GetItemsFromNode(XmlNode node)
        {
            return new List<Item>(
                from XmlNode n in node.ChildNodes
                where n.Name == XmlKey
                select new Item(n));
        }

        public override string ToString() => $"{Name}: {Type} item, {Weight ?? 0} lbs.\n{Text}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
