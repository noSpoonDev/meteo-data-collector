using System.Xml;
using Newtonsoft.Json;

namespace MeteoDataCollector.Core.Services;

public class TransformXmlToJsonStrategy : ITransformToJsonStrategy
{
    public string Transform(string input)
    {
        var doc = new XmlDocument();
        doc.LoadXml(input);

        return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, false);
    }
}