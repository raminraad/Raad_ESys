using ESys.API.Statics;
using ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.GenerateBusinessFormUrl
{
    /// <summary>
    /// Business holder uses this end point to request access token for a specific client session
    /// </summary>
    public class GenerateBusinessFormUrlEndPoint : Endpoint<GenerateBusinessFormUrlRequest, string>
    {
        private readonly IMediator _mediator;
        private readonly AutoMapper.IMapper _mapper;

        public GenerateBusinessFormUrlEndPoint(IMediator mediator, AutoMapper.IMapper mapper)
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
        public override async Task HandleAsync(GenerateBusinessFormUrlRequest req, CancellationToken ct)
        {
            try
            {
                var mediatorReq = _mapper.Map<GenerateBusinessFormUrlCommand>(req);

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