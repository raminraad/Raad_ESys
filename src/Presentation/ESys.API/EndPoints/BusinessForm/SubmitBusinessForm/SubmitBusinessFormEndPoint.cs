using System.Text.Json;
using System.Text.Json.Nodes;
using ESys.Application.Features.BusinessForm.Commands.SubmitBusinessForm;
using ESys.Application.Features.BusinessForm.Queries.CalculateBusinessForm;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.SubmitBusinessForm
{
    /// <summary>
    /// End point for getting submitted data and file names from client
    /// </summary>
    public class SubmitBusinessFormEndPoint : Endpoint<List<JsonObject>, string>
    {
        private readonly IMediator _mediator;

        public SubmitBusinessFormEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/business/submit");
            // AllowAnonymous();
            Roles("client");
        }

        /// <summary>
        /// Handles Business Form submission via mediator
        /// </summary>
        /// <param name="req">List of form inputs to be reevaluated and saved</param>
        /// <param name="ct">Cancellation token</param>
        public override async Task HandleAsync(List<JsonObject> req, CancellationToken ct)
        {
            try
            {
                string serializeObject = JsonSerializer.Serialize(req);

                var mediatorReq = new SubmitBusinessFormCommand
                {
                    SubmissionInput = serializeObject,
                    TempRoute = Guid.Parse(User.Claims.First(c => c.Type == "temp-route").Value),
                    ClientToken = User.Claims.First(c => c.Type == "client-token").Value
                };

                var submissionSucceeded = await _mediator.Send(mediatorReq, ct);

                if (submissionSucceeded)
                    await SendOkAsync("Submission succeeded.", ct);
                else
                    await SendErrorsAsync(cancellation: ct);
            }
            catch (Exception e)
            {
                await SendResultAsync(Results.BadRequest(e.Message));
            }
        }
    }
}