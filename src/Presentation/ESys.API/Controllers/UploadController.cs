using ESys.Application.Abstractions.Services.FileUploadHandler;
using Microsoft.AspNetCore.Mvc;

namespace ESys.API.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    private readonly IFileUploadHandlerService _fileUploadHandlerService;
    private readonly FileUploadHandlerConfig _fileUploadHandlerConfig;

    public UploadController(IFileUploadHandlerService fileUploadHandlerService,IConfiguration configuration)
    {
        this._fileUploadHandlerService = fileUploadHandlerService;
        var uploadHandlerConfig = configuration.GetSection("UploadHandlerConfig").Get<FileUploadHandlerConfig>() ;
        
        _fileUploadHandlerConfig = uploadHandlerConfig;
    }
    
    [HttpPost()]
    [Route("business/single/{businessId}/{orderId}")] //todo: get proper parameters
    public IActionResult BusinessFormUploadSingle(IFormFile file,string businessId,string orderId)
    {
        try
        {
            _fileUploadHandlerService.FileUploadHandlerConfig.UploadChildDirectory = $"Business\\{businessId}\\{orderId}";
            return Ok(new{dxsfile=_fileUploadHandlerService.Upload(file)});
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
            _fileUploadHandlerService.FileUploadHandlerConfig.UploadChildDirectory = $"Business\\{businessId}\\{orderId}";
            return Ok(_fileUploadHandlerService.Upload(files));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}