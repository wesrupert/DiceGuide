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

        public ObservableCollection<Character> Characters { get; private set; } = new ObservableCollection<Character>();
        public ObservableCollection<Race>      Races      { get; private set; } = new ObservableCollection<Race>();
        public ObservableCollection<Class>     Classes    { get; private set; } = new ObservableCollection<Class>();
        public ObservableCollection<Item>      Items      { get; private set; } = new ObservableCollection<Item>();

        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
