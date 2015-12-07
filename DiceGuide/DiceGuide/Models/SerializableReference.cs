using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace DiceGuide.Models
{
    abstract class SerializableReference : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Writes the object to the XML writer.
        /// </summary>
        /// <param name="writer">The writer to pass the object into.</param>
        public abstract void WriteToXML(XmlWriter writer);

        /// <inheritdoc />
        public abstract override string ToString();

        /// <summary>
        /// Gets the string value from the children of the given node.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        /// <param name="target">The target information type.</param>
        /// <param name="separator">The type of separator to distinguish multiple matches.</param>
        /// <returns>The string value containing all matches of target in node, separated by the given separator.</returns>
        protected static string GetStringFromNode(XmlNode node, string target, string separator = "")
        {
            return string.Join(
                separator,
                from XmlNode n in node.ChildNodes
                    where n.Name == target
                    select n.HasChildNodes ? n.FirstChild.Value : string.Empty);
        }

        /// <summary>
        /// Gets the unsigned numeric value from the children of the given node.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        /// <param name="target">The target information type.</param>
        /// <returns>The string value containing the relevant match of target in node.</returns>
        protected static uint? GetUnsignedFromNode(XmlNode node, string target)
        {
            bool matched = false;
            uint total = 0;
            foreach (string s in (from XmlNode n in node.ChildNodes where n.Name == target select n.FirstChild.Value))
            {
                uint w;
                if (uint.TryParse(s, out w))
                {
                    matched = true;
                    total += w;
                }
            }
            return matched ? (uint?)total : null;
        }

        /// <summary>
        /// Gets the signed numeric value from the children of the given node.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        /// <param name="target">The target information type.</param>
        /// <returns>The string value containing the relevant match of target in node.</returns>
        protected static int? GetSignedFromNode(XmlNode node, string target)
        {
            bool matched = false;
            int total = 0;
            foreach (string s in (from XmlNode n in node.ChildNodes where n.Name == target select n.FirstChild.Value))
            {
                int w;
                if (int.TryParse(s, out w))
                {
                    matched = true;
                    total += w;
                }
            }
            return matched ? (int?)total : null;
        }

        /// <summary>
        /// Property changed event caller.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
