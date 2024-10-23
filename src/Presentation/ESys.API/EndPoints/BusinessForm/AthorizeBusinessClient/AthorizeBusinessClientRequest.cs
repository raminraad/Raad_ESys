namespace ESys.API.EndPoints.BusinessForm.AthorizeBusinessClient;

public record AthorizeBusinessClientRequest()
{
    public required string BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
}