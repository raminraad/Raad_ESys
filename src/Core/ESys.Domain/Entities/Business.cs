namespace ESys.Domain.Entities;

public class Business
{
    public int BusinessId { set; get; }
    public string Title { set; get; } = string.Empty;
    public string Exp { set; get; } = string.Empty;
    public string Func { set; get; } = string.Empty;
    public string Lookup { set; get; } = string.Empty;
    public byte State { set; get; }
    public List<BusinessXml> BisunessXmls { get; set; } = [];
    public BusinessInitialUI? BusinessInitialUi { get; set; } = default;
}