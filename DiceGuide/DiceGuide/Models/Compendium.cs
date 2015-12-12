using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
        /// Loads the compendium with data in the given xml data.
        /// </summary>
        /// <param name="path">The path the the file to load.</param>
        public async Task<bool> LoadCompendium(string path)
        {
            XmlNode compendium = null;
            await Task.Run(() =>
            {
                try
                {
                    using (XmlReader reader = XmlReader.Create(path))
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(reader);
                        compendium = document.GetElementsByTagName("compendium")[0];
                    }
                }
                catch (FileNotFoundException)
                {
                    Debug.Fail($"File {path} not found!");
                }
            });

            if (compendium == null)
            {
                return false;
            }

            Characters.Clear();
            Character.GetCharactersFromNode(compendium).ForEach((c) => Characters.Add(c));

            Races.Clear();
            Race.GetRacesFromNode(compendium).ForEach((r) => Races.Add(r));

            Classes.Clear();
            Class.GetClassesFromNode(compendium).ForEach((c) => Classes.Add(c));

            Items.Clear();
            Item.GetItemsFromNode(compendium).ForEach((i) => Items.Add(i));

            return true;
        }

        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
