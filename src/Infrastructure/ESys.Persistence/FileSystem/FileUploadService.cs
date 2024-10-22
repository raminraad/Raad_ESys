using ESys.Application.Abstractions.Services.FileUpload;
using ESys.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ESys.Persistence.FileSystem;

public class FileUploadService : IFileUploadService
{
    private FileUploadConfigDto _fileUploadConfigDto;

    public FileUploadConfigDto FileUploadConfigDto
    {
        get => _fileUploadConfigDto;
        set => _fileUploadConfigDto = value;
    }

    public FileUploadService(IConfiguration configuration)
    {
        // todo: check if you can do this in DI container
        _fileUploadConfigDto = configuration.GetSection("UploadHandlerConfig").Get<FileUploadConfigDto>();
    }

    private bool CheckConstraints(IFormFile file)
    {
        //Checking the file for extension validation
        var fileExtension = Path.GetExtension(file.FileName);
        if (_fileUploadConfigDto.AcceptedExtensions.Count > 0 &&
            !_fileUploadConfigDto.AcceptedExtensions.Contains(fileExtension))
        {
            throw new FileUploadExtensionException(file);
        }

        //Checking the file for size validation
        long size = file.Length;
        var sizeLimit = _fileUploadConfigDto.MaxSizeInMB * 1024 * 1024;
        if (size > sizeLimit)
        {
            throw new FileUploadSizeLimitException(file, sizeLimit);
        }

        return true;
    }

    public string Receive(IFormFile file)
    {
        if (!CheckConstraints(file))
            return "File(s) not valid.";

        //name changing
        var fileExtension = Path.GetExtension(file.FileName);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            _fileUploadConfigDto.UploadRootDirectory, _fileUploadConfigDto.UploadChildDirectory);
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

    public IEnumerable<string> Receive(IEnumerable<IFormFile> files)
    {
        foreach (var file in files)
            yield return Receive(file);
    }
}