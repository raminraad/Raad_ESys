namespace ESys.Application.Abstractions.Services.BusinessFormCalculation;

public interface IExpHelper
{
    Dictionary<string, string> ApplyExpsOnData(Dictionary<string, string> dataDic, Dictionary<string, string> expDic);
}