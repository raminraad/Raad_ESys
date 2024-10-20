namespace ESys.API.EndPoints.BusinessForm.RedirectToBusinessForm;

public record RedirectToBusinessFormRequest()
{
    public required string BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
    public required string ClientSessionId { get; set; }
}