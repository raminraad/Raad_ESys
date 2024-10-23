using ESys.Application.Features.BusinessForm.Queries.InitiateBusinessForm;
using ESys.Domain.Exceptions;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.InitiateBusinessForm
{
    /// <summary>
    /// End point for getting data needed for Business From initialization
    /// </summary>
    public class InitiateBusinessFormEndPoint : Endpoint<InitiateBusinessFormQuery, string>
    {
        private readonly IMediator _mediator;

        public InitiateBusinessFormEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override void Configure()
        {
            Get("/business/initialize/{BusinessId}");
            // AllowAnonymous();
           Roles("client"); 
        }

        /// <summary>
        /// Handles Business Form initialization via mediator 
        /// </summary>
        /// <param name="req">Request containing Business Id needed for Business Form initialization</param>
        /// <param name="ct">Cancellation token</param>
        public override async Task HandleAsync(InitiateBusinessFormQuery req, CancellationToken ct)
        {
            try
            {
                var resp = await _mediator.Send(req,ct);

                await SendStringAsync(resp, cancellation: ct);
            }
            catch (NotFoundException e)
            { 
                await SendNotFoundAsync(ct);
            }
        }

    }
}