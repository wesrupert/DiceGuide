using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DiceGuide.Models
{
    class Race : SerializableNotifier
    {
        public override string ToString() => $"{Name}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
