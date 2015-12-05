﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DiceGuide.Models
{
    class Item : SerializableReference
    {

        private string _name = string.Empty;
        private string _type = string.Empty;
        private uint? _weight = null;
        private string _text = string.Empty;

        /// <summary>
        /// The item's name.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// The item's type.
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        /// <summary>
        /// The item's weight.
        /// </summary>
        public uint? Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }

        /// <summary>
        /// The item's description.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        /// <summary>
        /// Creates an empty item.
        /// </summary>
        public Item() { }

        /// <summary>
        /// Creates a new item from import data.
        /// </summary>
        /// <param name="node"></param>
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

        public override string ToString()
        {
            return string.Format("{0}: {1} item, {2} lbs.\n{3}", Name, Type, Weight.HasValue ? Weight.Value.ToString() : "?", Text);
        }

        public override void WriteToXML(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
