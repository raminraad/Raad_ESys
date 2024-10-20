using ESys.Application.Abstractions.Services.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace ESys.API.Controllers;

[ApiController]
[Route("api/upload")]
//todo : remove unused controller
public class UploadController : ControllerBase
{
    private readonly IFileUploadService _fileUploadService;
    private readonly FileUploadConfigDto _fileUploadConfigDto;

    public UploadController(IFileUploadService fileUploadService,IConfiguration configuration)
    {
        this._fileUploadService = fileUploadService;
        var uploadHandlerConfig = configuration.GetSection("UploadHandlerConfig").Get<FileUploadConfigDto>() ;
        
        _fileUploadConfigDto = uploadHandlerConfig;
    }
    
    [HttpPost()]
    [Route("business/single/{businessId}/{orderId}")] //todo: get proper parameters
    public IActionResult BusinessFormUploadSingle(IFormFile file,string businessId,string orderId)
    {
        try
        {
            _fileUploadService.FileUploadConfigDto.UploadChildDirectory = $"Business\\{businessId}\\{orderId}";
            return Ok(new{dxsfile=_fileUploadService.Upload(file)});
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost()]
    [Route("business/multiple/{businessId}/{orderId}")]
    public IActionResult BusinessUploadMultiple(IEnumerable<IFormFile> files,string businessId,string orderId)
    {
        try
        {
            _fileUploadService.FileUploadConfigDto.UploadChildDirectory = $"Business\\{businessId}\\{orderId}";
            return Ok(_fileUploadService.Upload(files));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}