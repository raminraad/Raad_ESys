namespace ESys.Application.CQRS.BusinessForm.Queries.GetInitialBusinessForm;

/// <summary>
/// Response being sent back to client containing initialization data for Business Form
/// </summary>
public class GetInitialBusinessFormQueryResult
{
    /// <summary>
    /// Contains a Json string filled with needed data for Business Form initialization
    /// </summary>
    public string Result { get; set; } = string.Empty;
}