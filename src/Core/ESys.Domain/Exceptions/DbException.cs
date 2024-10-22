namespace ESys.Domain.Exceptions;

public class DbException : Exception
{
    public DbException()
    {
        
    }

    public DbException(string message) : base( message)
    {
        
    }
}