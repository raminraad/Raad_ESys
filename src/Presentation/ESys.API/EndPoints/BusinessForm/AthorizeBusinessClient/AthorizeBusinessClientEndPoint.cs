using ESys.API.Statics;
using ESys.Application.Features.BusinessForm.Commands.AthorizeBusinessClient;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.AthorizeBusinessClient
{
    /// <summary>
    /// Business holder uses this end point to request access token for a specific client session
    /// A temporary Url to business form will be returned in case on successful authorization
    /// </summary>
    public class AthorizeBusinessClientEndPoint : Endpoint<AthorizeBusinessClientRequest, string>
    {
        private readonly IMediator _mediator;
        private readonly AutoMapper.IMapper _mapper;

        public AthorizeBusinessClientEndPoint(IMediator mediator, AutoMapper.IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/business/auth/client");
            AllowAnonymous();
        }

        /// <summary>
        /// Generates access token and redirection Url for business page
        /// </summary>
        /// <param name="req">Needed information for request validation</param>
        /// <param name="ct">Cancellation token</param>
        public override async Task HandleAsync(AthorizeBusinessClientRequest req, CancellationToken ct)
        {
            try
            {
                var mediatorReq = _mapper.Map<AthorizeBusinessClientCommand>(req);

                var resp = await _mediator.Send(mediatorReq, ct);

                var redirectionUrl = $"{ApiStatics.BusinessFormBaseUrl}/{resp.TempRoute}";
                await SendOkAsync(redirectionUrl, ct);
            }
            catch (Exception e)
            {
                await SendResultAsync(Results.BadRequest(e.Message));
            }
        }
    }
}