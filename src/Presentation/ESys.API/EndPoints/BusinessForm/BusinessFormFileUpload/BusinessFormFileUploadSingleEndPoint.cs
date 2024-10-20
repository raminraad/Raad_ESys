using ESys.Application.Contracts.Services.FileUploadHandler;
using ESys.Application.Models;
using FastEndpoints;

namespace ESys.API.EndPoints.BusinessForm.BusinessFormFileUpload
{
    /// <summary>
    /// End point for uploading files needed for initializing Business form
    /// </summary>
    public class BusinessFormFileUploadSingleEndPoint : Endpoint<BusinessFormFileUploadRequest,BusinessFormFileUploadResponse>
    {
        private readonly IFileUploadHandlerService _fileUploadHandlerService;
        private readonly FileUploadHandlerConfig _fileUploadHandlerConfig;

        public BusinessFormFileUploadSingleEndPoint(IFileUploadHandlerService fileUploadHandlerService,IConfiguration configuration)
        {
            _fileUploadHandlerService = fileUploadHandlerService;
            var uploadHandlerConfig = configuration.GetSection("UploadHandlerConfig").Get<FileUploadHandlerConfig>() ;
            _fileUploadHandlerConfig = uploadHandlerConfig ?? new();
        }

        
        public override void Configure()
        {
            Post("/upload/calc/single");
            AllowFileUploads();
        }

        public override async Task HandleAsync(BusinessFormFileUploadRequest req, CancellationToken ct)
        {
                _fileUploadHandlerService.FileUploadHandlerConfig.UploadChildDirectory = $"Biz\\{req.BusinessId}\\{req.OrderId}";
            
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
                var fileName = _fileUploadHandlerService.Upload(file);
            await SendAsync(new BusinessFormFileUploadResponse(){Dxsfile = fileName});
            }
        }


    }
}