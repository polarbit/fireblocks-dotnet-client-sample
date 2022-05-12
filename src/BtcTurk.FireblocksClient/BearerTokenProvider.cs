namespace BtcTurk.FireblocksClient;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class BearerTokenProvider
{
    private readonly SigningCredentials _signingCredentials;

    public string Apikey { get; }

    public BearerTokenProvider(string apikey, string privateKey)
    {
        Apikey = apikey;

        var rsa = new RSACryptoServiceProvider();
        rsa.ImportFromPem(privateKey);

        var securityKey = new RsaSecurityKey(rsa);
        _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);
    }

    public string GenerateJwtToken(HttpRequestMessage msg)
    {
        var payload = PreparePayload(msg);
        var token = new JwtSecurityToken(new JwtHeader(_signingCredentials), payload);

        var tokenokenHandler = new JwtSecurityTokenHandler();
        return tokenokenHandler.WriteToken(token);
    }

    private JwtPayload PreparePayload(HttpRequestMessage msg)
    {
        var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var issuedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var expirationTimestamp = issuedTimestamp + 20;
        var body = msg.Content?.ReadAsStringAsync().GetAwaiter().GetResult() ?? string.Empty;
        var bodyHash = ComputeHash(body);
        
        return new JwtPayload
        {
            {"uri", msg.RequestUri!.PathAndQuery},
            {"nonce", nonce},
            {"iat", issuedTimestamp},
            {"exp", expirationTimestamp},
            {"sub", Apikey},
            {"bodyHash", bodyHash}
        };
    }

    private string ComputeHash(string msg)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(msg));
        return Convert.ToHexString(bytes).ToLower();
    }
}