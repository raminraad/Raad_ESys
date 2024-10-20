namespace ESys.Domain.Entities;

public class Business
{
    public string BizId { set; get; } = string.Empty;
    public string Exp { set; get; } = string.Empty;
    public string Func { set; get; } = string.Empty;
    public string Lookup { set; get; } = string.Empty;
    public int State { set; get; }
    public string Name { set; get; } = string.Empty;
    public List<BusinessXml> BizXmls { get; set; } = [];
    public BusinessInitialUI? UI { get; set; } = default;
}