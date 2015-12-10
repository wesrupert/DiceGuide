using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DiceGuide.Models
{
    class Class : SerializableNotifier
    {
        /// <summary>The class' level.</summary>
        public uint? Level { get { return _level; } set { SetField(ref _level, value); } }
        protected uint? _level = null;

        public override string ToString() => $"Level {Level ?? 0} {Name}";
        public override void WriteToXML(XmlWriter writer) => new NotImplementedException();
    }
}
