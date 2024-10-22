using Microsoft.AspNetCore.Http;

namespace ESys.Application.Abstractions.Services.FileUpload;

public interface IFileUploadService
{
    FileUploadConfigDto FileUploadConfigDto { set; get; }
    string Receive(IFormFile file);
    IEnumerable<string> Receive(IEnumerable<IFormFile> files);

}