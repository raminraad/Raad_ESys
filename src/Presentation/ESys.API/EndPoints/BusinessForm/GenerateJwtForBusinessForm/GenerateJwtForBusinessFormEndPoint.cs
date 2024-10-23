using System.Text.Json;
using ESys.Application.Features.BusinessForm.Commands.GenerateJwtForBusinessForm;
using FastEndpoints;
using MediatR;

namespace ESys.API.EndPoints.BusinessForm.GenerateJwtForBusinessForm
{
    /// <summary>
    /// End point for generating JWT on calculation form initial load
    /// </summary>
    public class GenerateJwtForBusinessFormEndPoint : Endpoint<GenerateJwtForBusinessFormRequest, GenerateJwtForBusinessFormResponse>
    {
        private readonly IMediator _mediator;
        private readonly AutoMapper.IMapper _mapper;

        public GenerateJwtForBusinessFormEndPoint(IMediator mediator, AutoMapper.IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/businessjwt");
            AllowAnonymous();
        }

        /// <summary>
        /// Generates a JWT for client to use while working with business calculation form
        /// </summary>
        /// <param name="req">Needed information for generating JWT</param>
        /// <param name="ct">Cancellation token</param>
        public override async Task HandleAsync(GenerateJwtForBusinessFormRequest req, CancellationToken ct)
        {
            
            try
            {
                //todo: check if you can use fast end-points built-in mapper
                var mediatorReq = _mapper.Map<GenerateJwtForBusinessFormCommand>(req);

                var resp = await _mediator.Send(mediatorReq, ct);

                await SendOkAsync(new GenerateJwtForBusinessFormResponse {Token = resp},ct);
            }
            catch (Exception e)
            {
                await SendResultAsync(Results.BadRequest(e.Message));
            }
        }
    }
}