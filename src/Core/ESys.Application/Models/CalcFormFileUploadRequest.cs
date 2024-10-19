using Microsoft.AspNetCore.Mvc;

namespace ESys.Application.Models;

public class CalcFormFileUploadRequest
{
    public string BizId { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
}