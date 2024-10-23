namespace ESys.Persistence.Static;

/// <summary>
/// Static values containing table names in SQL Server. Mostly used by Dapper
/// </summary>
public static class SqlServerStatics
{
    public static class ConnectionStrings
    {
        public static string BusinessConnectionStringName => "EPaaSSqlServerConnectionString";
    }

    public static class Tables
    {
        public static class TblBusiness
        {
        public static string TableName => "dbo.tblBusiness";
        public static string BusinessId => "BusinessId";
        public static string Title => "Title";
        public static string Exp => "Exp";
        public static string Func => "Func";
        public static string Lookup => "Lookup";
        public static string State => "State";
        }
public static class TblBusinessXml
        {
        public static string TableName => "dbo.tblBusinessXml";
        public static string BusinessXmlId => "BusinessXmlId";
        public static string BusinessId => "BusinessId";
        public static string TName => "TName";
        public static string Xml => "Xml";
        public static string XmlTitles => "XmlTitles";
        public static string XmlTags => "XmlTags";
        public static string XmlColCount => "XmlColCount";
        public static string WhereClause => "WhereClause";
        }

        public static class TblBusinessInitialUi
        {
            public static string TableName => "dbo.tblBusinessInitialUi";
            public static string BusinessInitialUiId => "BusinessInitialUiId";
            public static string BusinessId => "BusinessId";
            public static string UiContent => "UiContent";
        }
        
        public static class TblClientSessionCache
        {
            public static string TableName => "dbo.tblClientSessionCache";
            public static string ClientSessionCacheId => "ClientSessionCacheId";
            public static string BusinessId => "BusinessId";
            public static string TempRoute => "TempRoute";
            public static string ClientToken => "ClientToken";
        }
        
        public static class TblClientSessionHistory
        {
            public static string TableName => "dbo.tblClientSessionHistory";
            public static string ClientSessionHistoryId => "ClientSessionHistoryId";
            public static string BusinessId => "BusinessId";
            public static string TempRoute => "TempRoute";
            public static string ClientToken => "ClientToken";
            public static string SubmissionInput => "SubmissionInput";
            public static string SubmissionOutput => "SubmissionOutput";
        }
    }
}