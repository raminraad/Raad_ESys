namespace ESys.Application.Abstractions.Services.FileUploadHandler;

public class FileUploadConfigDto
{
    public int MaxSizeInMB { get; set; } = 10;
    public List<string> AcceptedExtensions { get; set; } = [];
    public string UploadRootDirectory { get; set; } = "ClientUploads";
    public string UploadChildDirectory { get; set; } = string.Empty;
}