namespace Demo.AuthService.Models
{
    public class Tokens
    {
        /// <summary>User access token</summary>
        /// <example>some_access_token_value</example>
        public string Token { get; set; }

        /// <summary>User refresh token</summary>
        /// <example>some_refresh_token_value</example>
        public string RefreshToken { get; set; }
    }
}
