using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EXX_CP_FacturacionMasiva.Domain.Entities
{
    [XmlRoot(ElementName ="DBData")]
    public class DBData
    {
        [XmlArray("Queries")]
        [XmlArrayItem("Query", typeof(Query))]
        public Query[] Queries { get; set; }
    }

    public class Query
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }
        [XmlArray("Scripts")]
        [XmlArrayItem("Script", typeof(Script))]
        public Script[] Scripts { get; set; }
        [XmlArray("Params")]
        [XmlArrayItem("Param", typeof(Param))]
        public Param[] Params { get; set; }
    }

    public class Script
    {
        [XmlAttribute("DBType")]
        public string DBType { get; set; }
        [XmlAttribute("Type")]
        public string Type { get; set; }
        [XmlElement("Value")]
        public string Value { get; set; }
    }

    public class Param
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlAttribute("Value")]
        public string Value { get; set; }
    }
}
