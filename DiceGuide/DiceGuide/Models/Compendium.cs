using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DiceGuide.Models
{
    class Compendium : SerializableNotifier
    {
        /// <summary>The active comendium instance.</summary>
        public static Compendium Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Compendium();
                }
                return _instance;
            }
        }
        private static Compendium _instance;
        private Compendium() { }

        /// <summary>The characters in the compendium.</summary>
        public ObservableCollection<Character> Characters { get; private set; } = new ObservableCollection<Character>();

        /// <summary>The races in the compendium.</summary>
        public ObservableCollection<Race> Races { get; private set; } = new ObservableCollection<Race>();

        /// <summary>The classes in the compendium.</summary>
        public ObservableCollection<Class> Classes { get; private set; } = new ObservableCollection<Class>();

        /// <summary>The items in the compendium.</summary>
        public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();

        /// <summary>
        /// Loads the compendium with data in the given XmlNode.
        /// </summary>
        /// <param name="node">The node containing the desired information.</param>
        public void LoadCompendium(XmlNode node)
        {
            Characters.Clear();
            Character.GetCharactersFromNode(node).ForEach((c) => Characters.Add(c));

            Races.Clear();
            Race.GetRacesFromNode(node).ForEach((r) => Races.Add(r));

            Classes.Clear();
            Class.GetClassesFromNode(node).ForEach((c) => Classes.Add(c));

            Items.Clear();
            Item.GetItemsFromNode(node).ForEach((i) => Items.Add(i));
        }

        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
