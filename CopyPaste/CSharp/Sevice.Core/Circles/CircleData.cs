using System.Xml.Serialization;

namespace Services.Core.Circles
{
    [XmlRoot("Circles", Namespace = "", IsNullable = false)]
    [Serializable]
    public class CirclesData
    {
        [XmlElement("Circle")]
        public List<CircleData> Circles { get; set; }
    }

    [Serializable]
    public class CircleData
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("Position")]
        public CirclePositionData Position { get; set; }

        [XmlElement("FillColor")]
        public string FillColor { get; set; }

        [XmlElement("StrokeColor")]
        public string StrokeColor { get; set; }
    }

    [Serializable]
    public class CirclePositionData
    {
        [XmlAttribute("X")]
        public double X { get; set; }

        [XmlAttribute("Y")]
        public double Y { get; set; }
    }
}
