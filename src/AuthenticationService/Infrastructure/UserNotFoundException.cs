namespace Demo.AuthService.Infrastructure
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userName)
            : base($"User with name {userName} cannot be found")
        { }
    }
}
