namespace VertexERP.Application.Modules.Identity.Authentication.Register;

public class RegisterResponse
{
    public int UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiration { get; set; }
    public DateTime AccessTokenExpiration { get; set; }

}

