using ESys.Application.Models;
using ESys.Application.Services.FileUploadHandler;
using FastEndpoints;

namespace ESys.API.EndPoints.CalcFormFileUpload
{
    /// <summary>
    /// End point for uploading files needed for initializing Biz form
    /// </summary>
    public class CalcFormFileUploadSingleEndPoint : Endpoint<CalcFormFileUploadRequest,CalcFormFileUploadResponse>
    {
        private readonly IFileUploadHandlerService _fileUploadHandlerService;
        private readonly FileUploadHandlerConfig _fileUploadHandlerConfig;

        public CalcFormFileUploadSingleEndPoint(IFileUploadHandlerService fileUploadHandlerService,IConfiguration configuration)
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

        public override async Task HandleAsync(CalcFormFileUploadRequest req, CancellationToken ct)
        {
                _fileUploadHandlerService.FileUploadHandlerConfig.UploadChildDirectory = $"Biz\\{req.BizId}\\{req.OrderId}";
            
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
            await SendAsync(new CalcFormFileUploadResponse(){Dxsfile = fileName});
            }
        }


    }
}