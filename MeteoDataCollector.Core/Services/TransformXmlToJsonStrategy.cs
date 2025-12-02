using System.Xml;
using MeteoDataCollector.Core.Services;
using Newtonsoft.Json;

public class TransformXmlToJsonStrategy : ITransformToJsonStrategy
{
    public string Transform(string input)
    {
        var doc = new XmlDocument();
        doc.LoadXml(input);

        return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, false);
    }
}