﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace DiceGuide.Models
{
    public abstract class SerializableNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>The object's name.</summary>
        public string Name { get { return _name; } set { SetField(ref _name, value); } }
        protected string _name = string.Empty;

        /// <summary>The object's text content.</summary>
        public string Text { get { return _text; } set { SetField(ref _text, value); } }
        protected string _text = string.Empty;

        /// <summary>Create a new SerializableNotifier.</summary>
        protected SerializableNotifier() { }

        /// <summary>
        /// Create a new SerializableNotifier.
        /// </summary>
        /// <param name="node">The XML node containing the desired data.</param>
        /// <param name="xmlkey">The XML key used to retrieve instances from XML files.</param>
        protected SerializableNotifier(XmlNode node, string xmlkey)
        {
            Debug.Assert(node.Name == xmlkey, $"Initializing {xmlkey} with wrong node type!");
            Debug.Assert(node.HasChildNodes, $"Initializing {xmlkey} with empty node!");
            if (node.Name != xmlkey || !node.HasChildNodes)
                return;

            _name = GetStringFromNode(node, "name");
            _text = GetStringFromNode(node, "text", "\n");
        }

        /// <summary>
        /// Writes the object to the XML writer.
        /// </summary>
        /// <param name="writer">The writer to pass the object into.</param>
        public abstract void WriteToXML(XmlWriter writer);

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
        /// Setter for INotifyPropertyChanged properties.
        /// </summary>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propertyName">The name of the property being assigned to.</param>
        /// <returns>True if the value was set, otherwise false if the value was identical to the previous one.</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Caller for the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
