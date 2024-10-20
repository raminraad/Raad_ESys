using Microsoft.AspNetCore.Http;

namespace ESys.Application.Abstractions.Services.FileUploadHandler;

public interface IFileUploadHandlerService
{
    FileUploadHandlerConfig FileUploadHandlerConfig { set; get; }
    string Upload(IFormFile file);
    IEnumerable<string> Upload(IEnumerable<IFormFile> files);

}