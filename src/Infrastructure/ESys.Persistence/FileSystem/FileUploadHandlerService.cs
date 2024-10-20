using ESys.Application.Contracts.Services.FileUploadHandler;
using ESys.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ESys.Persistence.FileSystem;

public class FileUploadHandlerService : IFileUploadHandlerService
{
    private FileUploadHandlerConfig _fileUploadHandlerConfig;

    public FileUploadHandlerConfig FileUploadHandlerConfig
    {
        get => _fileUploadHandlerConfig;
        set => _fileUploadHandlerConfig = value;
    }

    public FileUploadHandlerService(IConfiguration configuration)
    {
        // todo: check if you can do this in DI container
        _fileUploadHandlerConfig = configuration.GetSection("UploadHandlerConfig").Get<FileUploadHandlerConfig>();
    }

    private bool IsFileValid(IFormFile file)
    {
        //Checking the file for extension validation
        var fileExtension = Path.GetExtension(file.FileName);
        if (_fileUploadHandlerConfig.AcceptedExtensions.Count > 0 &&
            !_fileUploadHandlerConfig.AcceptedExtensions.Contains(fileExtension))
        {
            throw new FileUploadExtensionException(file);
        }

        //Checking the file for size validation
        long size = file.Length;
        var sizeLimit = _fileUploadHandlerConfig.MaxSizeInMB * 1024 * 1024;
        if (size > sizeLimit)
        {
            throw new FileUploadSizeLimitException(file, sizeLimit);
        }

        return true;
    }

    public string Upload(IFormFile file)
    {
        if (!IsFileValid(file))
            return "File(s) not valid.";

        //name changing
        var fileExtension = Path.GetExtension(file.FileName);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            _fileUploadHandlerConfig.UploadRootDirectory, _fileUploadHandlerConfig.UploadChildDirectory);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        var fileNewName = file.FileName.Remove(file.FileName.Length - fileExtension.Length);
        while (File.Exists(Path.Combine(path, fileNewName + fileExtension)))
        {
            fileNewName += "_";
        }

        using var stream = new FileStream(Path.Combine(path, fileNewName + fileExtension), FileMode.Create);
        file.CopyTo(stream);
        return fileNewName + fileExtension;
    }

    public IEnumerable<string> Upload(IEnumerable<IFormFile> files)
    {
        foreach (var file in files)
            yield return Upload(file);
    }
}