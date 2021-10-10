namespace VA.Identity.Application.Jwt.Model
{
    public class UserResponse<TKey>
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken<TKey> UserToken { get; set; }
    }

    public class UserResponse : UserResponse<string>
    {

    }
}
