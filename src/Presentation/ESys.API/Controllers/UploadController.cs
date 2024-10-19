using ESys.Application.Services.FileUploadHandler;
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
    [Route("biz/single/{bizId}/{orderId}")]
    public IActionResult BizUploadSingle(IFormFile file,string bizId,string orderId)
    {
        try
        {
            _fileUploadHandlerService.FileUploadHandlerConfig.UploadChildDirectory = $"Biz\\{bizId}\\{orderId}";
            return Ok(new{dxsfile=_fileUploadHandlerService.Upload(file)});
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost()]
    [Route("biz/multiple/{bizId}/{orderId}")]
    public IActionResult BizUploadMultiple(IEnumerable<IFormFile> files,string bizId,string orderId)
    {
        try
        {
            _fileUploadHandlerService.FileUploadHandlerConfig.UploadChildDirectory = $"Biz\\{bizId}\\{orderId}";
            return Ok(_fileUploadHandlerService.Upload(files));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}