using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace DiceGuide.Models
{
    class Character : SerializableNotifier
    {
        /// <summary>The XML key used to retrieve instances from XML files.</summary>
        private const string XmlKey = "character";

        /// <summary>The character's race.</summary>
        public Race Race { get { return _race; } set { SetField(ref _race, value); } }
        protected Race _race = null;

        /// <summary>The character's classes.</summary>
        public List<Class> Classes { get { return _classes; } set { SetField(ref _classes, value); } }
        protected List<Class> _classes = new List<Class>();

        /// <summary>The character's items.</summary>
        public List<Item> Items { get { return _items; } set { SetField(ref _items, value); } }
        protected List<Item> _items = new List<Item>();

        /// <summary>
        /// Creates a new character from import data.
        /// </summary>
        /// <param name="node">The XmlNode containing the character data.</param>
        public Character(XmlNode node) : base(node, XmlKey)
        {
            _race = Race.GetRacesFromNode(node).FirstOrDefault();
            _classes = Class.GetClassesFromNode(node);
            _items = Item.GetItemsFromNode(node);
        }

        /// <summary>
        /// Gets the characters from the children of the given node.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        /// <returns>List containing all characters found in the node.</returns>
        internal static List<Character> GetCharactersFromNode(XmlNode node)
        {
            return new List<Character>(
                from XmlNode n in node.ChildNodes
                where n.Name == XmlKey
                select new Character(n));
        }

        public override string ToString() => $"{Name}: {Race} ({string.Join(", ", Classes)})\n{Text}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
