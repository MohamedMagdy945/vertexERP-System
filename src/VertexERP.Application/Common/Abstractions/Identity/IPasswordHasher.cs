namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IPasswordHasherService
{
    public string Hash(string password);

    public bool Verify(string password, string hashedPassword);
}

