namespace ESys.Application.CQRS.BusinessForm.Queries.GetCalculatedBusinessForm;

/// <summary>
/// Response being sent back to client containing calculated data for Business Form
/// </summary>
public class GetCalculatedBusinessFormResponse
{
    /// <summary>
    /// Contains a Json string filled with calculated data for Business Form
    /// </summary>
    public string Result { get; set; } 
}