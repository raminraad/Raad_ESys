namespace ESys.Domain.Entities;

public class ClientSessionHistory
{
    public uint ClientSessionHistoryId { get; init; }
    public int BusinessId { get; set; }
    public Guid TempRoute { init; get; }
    public required string ClientToken { get; set; }
    public Business? Business { get; set; } = default;
    public string? SubmissionInput { get; set; }
    public string? SubmissionOutput { get; set; }
}