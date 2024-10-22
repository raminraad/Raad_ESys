using System.Numerics;

namespace ESys.Domain.Entities;

public class BusinessXml
{
    public int BusinessXmlId { set; get; }
    public int BusinessId { set; get; }
    public string TName { set; get; } = string.Empty;
    public string Xml { set; get; } = string.Empty;
    public string XmlTitles { set; get; } = string.Empty;
    public string XmlTags { set; get; } = string.Empty;
    public string XmlColCount { set; get; } = string.Empty;
    public string WhereClause { set; get; } = string.Empty;
    public Business? Business { get; set; } = default;
}