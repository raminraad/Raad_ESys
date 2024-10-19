using Microsoft.AspNetCore.Http;

namespace ESys.Application.Services.FileUploadHandler;

public interface IFileUploadHandlerService
{
    FileUploadHandlerConfig FileUploadHandlerConfig { set; get; }
    string Upload(IFormFile file);
    IEnumerable<string> Upload(IEnumerable<IFormFile> files);

}