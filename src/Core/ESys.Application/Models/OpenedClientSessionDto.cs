namespace ESys.Application.Models;

public class OpenedClientSessionDto
{
        public Guid SessionId { init; get; }
        public required double Order { init; get; }
        public required string Url { get; set; }
}