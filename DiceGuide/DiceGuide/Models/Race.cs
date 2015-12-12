using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DiceGuide.Models
{
    class Race : SerializableNotifier
    {
        /// <summary>The XML key used to retrieve instances from XML files.</summary>
        private const string XmlKey = "race";

        /// <summary>
        /// Creates a new class from import data.
        /// </summary>
        /// <param name="node">The XmlNode containing the item data.</param>
        public Race(XmlNode node) : base(node, XmlKey)
        {
        }

        /// <summary>
        /// Gets the races from the children of the given node.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        /// <returns>List containing all races found in the node.</returns>
        internal static List<Race> GetRacesFromNode(XmlNode node)
        {
            return new List<Race>(
                from XmlNode n in node.ChildNodes
                where n.Name == XmlKey
                select new Race(n));
        }

        public override string ToString() => $"{Name}\n{Text}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
