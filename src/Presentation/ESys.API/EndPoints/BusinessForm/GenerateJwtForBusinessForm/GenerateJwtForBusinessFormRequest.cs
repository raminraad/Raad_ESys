namespace ESys.API.EndPoints.BusinessForm.GenerateJwtForBusinessForm;

public record GenerateJwtForBusinessFormRequest()
{
    public required string BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
    public required string ClientSessionId { get; set; }
}