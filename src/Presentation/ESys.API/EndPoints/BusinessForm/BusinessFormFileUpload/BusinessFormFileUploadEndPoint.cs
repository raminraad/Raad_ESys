using ESys.Application.Abstractions.Services.FileUpload;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace ESys.API.EndPoints.BusinessForm.BusinessFormFileUpload
{
    /// <summary>
    /// End point for uploading files needed for initializing Business form
    /// </summary>
    public class BusinessFormFileUploadEndPoint : Endpoint<BusinessFormFileUploadRequest,BusinessFormFileUploadResponse>
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly FileUploadConfigDto _fileUploadConfigDto;

        public BusinessFormFileUploadEndPoint(IFileUploadService fileUploadService,IConfiguration configuration)
        {
            _fileUploadService = fileUploadService;
            var uploadHandlerConfig = configuration.GetSection("UploadHandlerConfig").Get<FileUploadConfigDto>() ;
            _fileUploadConfigDto = uploadHandlerConfig ?? new();
        }

        
        public override void Configure()
        {
            Post("/upload/businessform/single");
            // Policies("user");
            AllowFileUploads();
            AllowAnonymous();
        }

        public override async Task HandleAsync(BusinessFormFileUploadRequest req, CancellationToken ct)
        {
                _fileUploadService.FileUploadConfigDto.UploadChildDirectory = $"{req.BusinessId}\\{req.TempRoute}";
            
            if (Files.Count > 0)
            {
                var file = Files[0];
                //
                // await SendStreamAsync(
                //     stream: file.OpenReadStream(),
                //     fileName: "test.png",
                //     fileLengthBytes: file.Length,
                //     contentType: "image/png");
                //
                // return;
                var fileName = _fileUploadService.Receive(file);
            await SendOkAsync(new BusinessFormFileUploadResponse {Dxsfile = fileName},ct);
            }
        }


    }
}