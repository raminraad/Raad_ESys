using ESys.Application.Features.BusinessForm.Queries.GetInitialBusinessForm;
using ESys.Domain.Exceptions;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.GetInitialBusinessForm
{
    /// <summary>
    /// End point for getting data needed for Business From initialization
    /// </summary>
    public class GetInitialBusinessFormEndPoint : Endpoint<GetInitialBusinessFormQuery, string>
    {
        private readonly IMediator _mediator;

        public GetInitialBusinessFormEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override void Configure()
        {
            Get("/business/initialize/{BusinessId}");
            AllowAnonymous();
           // Roles("client"); 
        }

        /// <summary>
        /// Handles Business Form initialization via mediator 
        /// </summary>
        /// <param name="req">Request containing Business Id needed for Business Form initialization</param>
        /// <param name="ct">Cancellation token</param>
        public override async Task HandleAsync(GetInitialBusinessFormQuery req, CancellationToken ct)
        {
            try
            {
                var resp = await _mediator.Send(req,ct);

                await SendStringAsync(resp.Result, cancellation: ct);
            }
            catch (NotFoundException e)
            { 
                await SendNotFoundAsync(ct);
            }
        }

    }
}