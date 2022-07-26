namespace Demo.AuthService.Infrastructure
{
    public static class ExceptionExtensions
    {
        public static string GetFullErrorMessage(this Exception ex)
        {
            return ex.InnerException == null 
                ? ex.Message
                : ex.Message + ". " + ex.InnerException.GetFullErrorMessage();
        }
    }
}
