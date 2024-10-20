namespace ESys.Domain.Entities;

public class BusinessInitialUI
{
    public string BizId { set; get; } = string.Empty;
    public string UiJson { set; get; } = string.Empty;
    public Business? Business { get; set; } = default;
}

