namespace VertexERP.Application.Models.Authentication;

public class AuthenticationResponse
{
    public int UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiration { get; set; }
}

