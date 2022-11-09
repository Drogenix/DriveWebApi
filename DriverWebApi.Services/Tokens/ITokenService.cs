namespace DriverWebApi.Services.Tokens
{
    public interface ITokenService
    {
        string CreateToken(string login);
    }
}