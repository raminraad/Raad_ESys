using System.Text.Json;
using System.Text.Json.Nodes;
using ESys.Application.Features.BusinessForm.Queries.GetCalculatedBusinessForm;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.GetCalculatedBusinessForm
{
    /// <summary>
    /// End point for getting data needed for initializing Business form
    /// </summary>
    public class GetCalculatedBusinessFormEndPoint : Endpoint<List<JsonObject>,GetCalculatedBusinessFormQueryResponse>
    {
        

        
        private readonly IMediator _mediator;

        public GetCalculatedBusinessFormEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/business/calculate");
            AllowAnonymous();
        }
        
        /// <summary>
        /// Handles Business Form reevaluation via mediator
        /// </summary>
        /// <param name="req">List of Json objects containing data needed for recalculation</param>
        /// <param name="ct">Cancellation token</param>
        public override async Task HandleAsync(List<JsonObject> req, CancellationToken ct)
        {
            
            var user = HttpContext.User;

            try
            {
                
                string serializeObject = JsonSerializer.Serialize(req);

                var mediatorReq = new GetCalculatedBusinessFormQuery { Body = serializeObject };

                var resp = await _mediator.Send(mediatorReq, ct);

                await SendOkAsync(resp,ct);

            }
            catch (Exception e)
            {
                await SendResultAsync(Results.BadRequest(e.Message));
            }
        }

    }
}