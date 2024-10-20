using System.Text.Json;
using System.Text.Json.Nodes;
using ESys.API.Models;
using ESys.Application.Features.BusinessForm.Queries.GetCalculatedBusinessForm;
using ESys.Authentication.JWT.BusinessForm;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.GenerateJwtForBusinessForm
{
    /// <summary>
    /// End point for generating JWT on calculation form initial load
    /// </summary>
    public class GenerateJwtForBusinessFormEndPoint : Endpoint<GenerateJwtForBusinessFormRequest,string>
    {
        private readonly IMediator _mediator;

        public GenerateJwtForBusinessFormEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/dyna");
            AllowAnonymous();
        }
        
        /// <summary>
        /// Handles Business Form reevaluation via mediator
        /// </summary>
        /// <param name="req">List of Json objects containing data needed for recalculation</param>
        /// <param name="ct">Cancellation token</param>
        public override async Task HandleAsync(GenerateJwtForBusinessFormRequest req, CancellationToken ct)
        {
            try
            {
                
                string serializeObject = JsonSerializer.Serialize(req);

                var mediatorReq = new GenerateJwtForCalcFormCommand(req.Email);

                var resp = await _mediator.Send(mediatorReq, ct);

                await SendAsync(resp.ToString());

            }
            catch (Exception e)
            {
                await SendResultAsync(Results.BadRequest(e.Message));
            }
        }

    }
}