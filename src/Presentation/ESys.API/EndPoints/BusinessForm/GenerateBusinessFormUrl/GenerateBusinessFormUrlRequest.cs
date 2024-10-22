namespace ESys.API.EndPoints.BusinessForm.GenerateBusinessFormUrl;

public record GenerateBusinessFormUrlRequest()
{
    public required string BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
}