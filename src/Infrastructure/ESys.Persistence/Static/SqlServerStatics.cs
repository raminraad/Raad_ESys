namespace ESys.Persistence.Static;
/// <summary>
/// Static values containing table names in SQL Server. Mostly used by Dapper
/// </summary>
public static class SqlServerStatics
{
    public static string BusinessTable => "dbo.tblBiz";
    public static string BusinessXmlTable => "dbo.tblBizXmls";
    public static string BusinessInitialUiTable => "dbo.tblUI";
}
