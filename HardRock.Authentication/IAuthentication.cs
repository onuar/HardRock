namespace HardRock.Authentication
{
    public interface IAuthentication
    {
        LoginResponseContext Login(LoginRequestContext requestContext);
    }
}