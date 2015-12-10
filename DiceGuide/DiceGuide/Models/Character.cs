using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace DiceGuide.Models
{
    class Character : SerializableNotifier
    {
        /// <summary>The character's race</summary>
        public Race Race { get { return _race; } set { SetField(ref _race, value); } }
        protected Race _race = new Race();

        /// <summary>The character's classes</summary>
        public List<Class> Classes { get { return _classes; } set { SetField(ref _classes, value); } }
        protected List<Class> _classes = new List<Class>();

        /// <summary>
        /// Creates a new character from import data.
        /// </summary>
        /// <param name="node">The XmlNode containing the character data.</param>
        public Character(XmlNode node)
        {
            Debug.Assert(node.Name == "character", "Initializing character with wrong node type!");
            Debug.Assert(node.HasChildNodes, "Initializing character with empty node!");
            if (node.Name != "character" || !node.HasChildNodes)
                return;

            _name = GetStringFromNode(node, "name");
            _description = GetStringFromNode(node, "bio", "\n");
            _race.Name = GetStringFromNode(node, "race");
            _classes = GetClassesFromNode(node);
        }

        /// <summary>
        /// Gets the signed numeric value from the children of the given node.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        /// <param name="target">The target information type.</param>
        /// <returns>The string value containing the relevant match of target in node.</returns>
        protected static List<Class> GetClassesFromNode(XmlNode node)
        {
            return new List<Class>(
                from XmlNode n in node.ChildNodes
                    where n.Name == "class"
                    select new Class()
                    {
                        Name = GetStringFromNode(n, "name"),
                        Level = GetUnsignedFromNode(n, "level"),
                    });
        }

        public override string ToString() => $"{Name}: {Race} ({string.Join(", ", Classes)})\n{Description}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
