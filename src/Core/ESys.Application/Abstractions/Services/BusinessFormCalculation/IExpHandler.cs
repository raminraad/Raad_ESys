namespace ESys.Application.Abstractions.Services.BusinessFormCalculation;

public interface IExpHandler
{
    Dictionary<string, string> ApplyExpsOnData(Dictionary<string, string> dataDic, Dictionary<string, string> expDic);
}