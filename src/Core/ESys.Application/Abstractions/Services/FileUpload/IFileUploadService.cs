using Microsoft.AspNetCore.Http;

namespace ESys.Application.Abstractions.Services.FileUpload;

public interface IFileUploadService
{
    FileUploadConfigDto FileUploadConfigDto { set; get; }
    string Upload(IFormFile file);
    IEnumerable<string> Upload(IEnumerable<IFormFile> files);

}