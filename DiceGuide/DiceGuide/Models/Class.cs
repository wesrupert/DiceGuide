using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DiceGuide.Models
{
    class Class : SerializableNotifier
    {
        /// <summary>The XML key used to retrieve instances from XML files.</summary>
        private const string XmlKey = "class";

        /// <summary>The class' level.</summary>
        public uint? Level { get { return _level; } set { SetField(ref _level, value); } }
        protected uint? _level = null;

        /// <summary>
        /// Creates a new class from import data.
        /// </summary>
        /// <param name="node">The XmlNode containing the item data.</param>
        public Class(XmlNode node) : base(node, XmlKey)
        {
            _level = GetUnsignedFromNode(node, "level");
        }

        /// <summary>
        /// Gets the classes from the children of the given node.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        /// <returns>List containing all classes found in the node.</returns>
        internal static List<Class> GetClassesFromNode(XmlNode node)
        {
            return new List<Class>(
                from XmlNode n in node.ChildNodes
                where n.Name == XmlKey
                select new Class(n));
        }

        public override string ToString() => $"Level {Level ?? 0} {Name}\n{Text}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
