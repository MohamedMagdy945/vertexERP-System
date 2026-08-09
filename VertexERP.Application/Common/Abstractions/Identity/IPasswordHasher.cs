namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IPasswordHasher
{
    public string Hash(string password);

    public bool Verify(string password, string hashedPassword);
}

