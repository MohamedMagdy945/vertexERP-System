namespace VertexERP.Application.Models.Authentication;

public class AuthenticationResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiration { get; set; }
}

