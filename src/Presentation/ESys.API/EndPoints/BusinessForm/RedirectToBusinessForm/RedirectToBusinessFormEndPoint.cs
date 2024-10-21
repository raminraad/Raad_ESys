using ESys.API.EndPoints.BusinessForm.GenerateJwtForBusinessForm;
using ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.RedirectToBusinessForm
{
    /// <summary>
    /// Business holder uses this end point to request access token for a specific client session
    /// </summary>
    public class RedirectToBusinessFormEndPoint : Endpoint<RedirectToBusinessFormRequest, RedirectToBusinessFormResponse>
    {
        private readonly IMediator _mediator;
        private readonly AutoMapper.IMapper _mapper;

        public RedirectToBusinessFormEndPoint(IMediator mediator, AutoMapper.IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/redirecttobusiness");
            AllowAnonymous();
        }

        /// <summary>
        /// Generates access token and redirection Url for business page
        /// </summary>
        /// <param name="req">Needed information for request validation</param>
        /// <param name="ct">Cancellation token</param>
        public override async Task HandleAsync(RedirectToBusinessFormRequest req, CancellationToken ct)
        {
            try
            {
                var mediatorReq = _mapper.Map<RedirectToBusinessFormCommand>(req);

                var resp = await _mediator.Send(mediatorReq, ct);

                await SendOkAsync(_mapper.Map<RedirectToBusinessFormResponse>(resp),ct);
            }
            catch (Exception e)
            {
                await SendResultAsync(Results.BadRequest(e.Message));
            }
        }
    }
}