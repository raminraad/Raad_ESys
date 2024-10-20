using ESys.Application.Abstractions.Services.FileUploadHandler;
using FastEndpoints;

namespace ESys.API.EndPoints.BusinessForm.BusinessFormFileUpload
{
    /// <summary>
    /// End point for uploading files needed for initializing Business form
    /// </summary>
    public class BusinessFormFileUploadSingleEndPoint : Endpoint<BusinessFormFileUploadRequest,BusinessFormFileUploadResponse>
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly FileUploadConfigDto _fileUploadConfigDto;

        public BusinessFormFileUploadSingleEndPoint(IFileUploadService fileUploadService,IConfiguration configuration)
        {
            _fileUploadService = fileUploadService;
            var uploadHandlerConfig = configuration.GetSection("UploadHandlerConfig").Get<FileUploadConfigDto>() ;
            _fileUploadConfigDto = uploadHandlerConfig ?? new();
        }

        
        public override void Configure()
        {
            Post("/upload/businessform/single");
            AllowFileUploads();
        }

        public override async Task HandleAsync(BusinessFormFileUploadRequest req, CancellationToken ct)
        {
                _fileUploadService.FileUploadConfigDto.UploadChildDirectory = $"Biz\\{req.BusinessId}\\{req.OrderId}";
            
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
                var fileName = _fileUploadService.Upload(file);
            await SendAsync(new BusinessFormFileUploadResponse(){Dxsfile = fileName});
            }
        }


    }
}