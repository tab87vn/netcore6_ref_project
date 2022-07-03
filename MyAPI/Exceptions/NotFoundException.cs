namespace tab.TestDotNet.API.Exceptions;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string? message) : base(message)
    {
    }
}
public class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(string? message) : base(message)
    {
    }
}