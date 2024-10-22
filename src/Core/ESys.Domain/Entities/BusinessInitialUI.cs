namespace ESys.Domain.Entities;

public class BusinessInitialUI
{
    public int BusinessInitialUIId { set; get; } 
    public int BusinessId { set; get; } 
    public string UiContent { set; get; } = string.Empty;
    public required Business Business { get; set; } 
}