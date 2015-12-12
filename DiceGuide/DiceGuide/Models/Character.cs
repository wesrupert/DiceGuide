using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DiceGuide.Models
{
    public class Character : SerializableNotifier
    {
        /// <summary>The XML key used to retrieve instances from XML files.</summary>
        private const string XmlKey = "character";

        /// <summary>The character's race.</summary>
        public Race Race { get { return _race; } set { SetField(ref _race, value); } }
        protected Race _race = null;

        /// <summary>The character's classes.</summary>
        public List<Class> Classes { get { return _classes; } set { SetField(ref _classes, value); } }
        protected List<Class> _classes = new List<Class>();

        /// <summary>String listing the character's classes.</summary>
        public string ClassString { get { return string.Join("/", Classes.Select((c) => $"{c.Name} {c.Level}")); } }

        /// <summary>The character's items.</summary>
        public List<Item> Items { get { return _items; } set { SetField(ref _items, value); } }
        protected List<Item> _items = new List<Item>();

        #region Character stats

        /// <summary>The character's maximum hp.</summary>
        public uint MaxHP { get { return _maxhp; } set { SetField(ref _maxhp, value); } }
        protected uint _maxhp = 0;

        /// <summary>The character's current hp.</summary>
        public uint CurrentHP { get { return _currenthp; } set { SetField(ref _currenthp, value); } }
        protected uint _currenthp = 0;

        /// <summary>The character's strength.</summary>
        public uint Strength
        {
            get { return _strength; }
            set
            {
                SetField(ref _strength, value);
                NotifyPropertyChanged("StrengthMod");
            }
        }
        protected uint _strength = 0;

        /// <summary>The character's strength modifier.</summary>
        public string StrengthModString { get { return $"{StatMod(Strength):+#;-#;+0}"; } }

        /// <summary>The character's dexterity.</summary>
        public uint Dexterity
        {
            get { return _dexterity; }
            set
            {
                SetField(ref _dexterity, value);
                NotifyPropertyChanged("DexterityMod");
            }
        }
        protected uint _dexterity = 0;

        /// <summary>The character's dexterity modifier.</summary>
        public string DexterityModString { get { return $"{StatMod(Dexterity):+#;-#;+0}"; } }

        /// <summary>The character's constitution.</summary>
        public uint Constitution
        {
            get { return _constitution; }
            set
            {
                SetField(ref _constitution, value);
                NotifyPropertyChanged("ConstitutionMod");
            }
        }
        protected uint _constitution = 0;

        /// <summary>The character's constitution modifier.</summary>
        public string ConstitutionModString { get { return $"{StatMod(Constitution):+#;-#;+0}"; } }

        /// <summary>The character's intelligence.</summary>
        public uint Intelligence
        {
            get { return _intelligence; }
            set
            {
                SetField(ref _intelligence, value);
                NotifyPropertyChanged("IntelligenceMod");
            }
        }
        protected uint _intelligence = 0;

        /// <summary>The character's intelligence modifier.</summary>
        public string IntelligenceModString { get { return $"{StatMod(Intelligence):+#;-#;+0}"; } }

        /// <summary>The character's wisdom.</summary>
        public uint Wisdom
        {
            get { return _wisdom; }
            set
            {
                SetField(ref _wisdom, value);
                NotifyPropertyChanged("WisdomMod");
            }
        }
        protected uint _wisdom = 0;

        /// <summary>The character's wisdom modifier.</summary>
        public string WisdomModString { get { return $"{StatMod(Wisdom):+#;-#;+0}"; } }

        /// <summary>The character's charisma.</summary>
        public uint Charisma
        {
            get { return _charisma; }
            set
            {
                SetField(ref _charisma, value);
                NotifyPropertyChanged("CharismaMod");
            }
        }
        protected uint _charisma = 0;

        /// <summary>The character's charisma modifier.</summary>
        public string CharismaModString { get { return $"{StatMod(Charisma):+#;-#;+0}"; } }

        #endregion

        /// <summary>
        /// Creates a new character from import data.
        /// </summary>
        /// <param name="node">The XmlNode containing the character data.</param>
        public Character(XmlNode node) : base(node, XmlKey)
        {
            _race = Race.GetRacesFromNode(node).FirstOrDefault();
            _classes = Class.GetClassesFromNode(node);
            _items = Item.GetItemsFromNode(node);

            _maxhp = GetUnsignedFromNode(node, "maxhp") ?? 0;
            _currenthp = GetUnsignedFromNode(node, "currenthp") ?? 0;

            _strength = GetUnsignedFromNode(node, "strength") ?? 10;
            _dexterity = GetUnsignedFromNode(node, "dexterity") ?? 10;
            _constitution = GetUnsignedFromNode(node, "constitution") ?? 10;
            _intelligence = GetUnsignedFromNode(node, "intelligence") ?? 10;
            _wisdom = GetUnsignedFromNode(node, "wisdom") ?? 10;
            _charisma = GetUnsignedFromNode(node, "charisma") ?? 10;
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

        /// <summary>
        /// Gets the stat modifier for a given stat.
        /// </summary>
        /// <param name="stat">The stat to find the modifier for.</param>
        /// <returns>The stat modifier for the given stat.</returns>
        protected int StatMod(uint stat)
        {
            return (int)(stat - 10) / 2;
        }

        public override string ToString() => $"{Name}: {Race} ({string.Join(", ", Classes)})\n{Text}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
