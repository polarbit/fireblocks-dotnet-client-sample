namespace BtcTurk.FireblocksClient;

using System.Net.Http.Headers;

public class AuthDelegatingHandler : DelegatingHandler
{
    private readonly BearerTokenProvider _bearerTokenProvider;

    public AuthDelegatingHandler(BearerTokenProvider bearerTokenProvider)
    {
        _bearerTokenProvider = bearerTokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _bearerTokenProvider.GenerateJwtToken(request);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Add("X-API-Key", _bearerTokenProvider.Apikey);
        return await base.SendAsync(request, cancellationToken);
    }
}