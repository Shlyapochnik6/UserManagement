namespace UserManagement.Application.Common.Exceptions;

public class UserExistsException : Exception
{
    public UserExistsException()
    {
        Console.WriteLine("The user already exists");
    }
}