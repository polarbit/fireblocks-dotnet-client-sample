namespace BtcTurk.FireblocksClient;

using System.Text.Json;
using System.Net.Http.Json;
using BtcTurk.FireblocksClient.Types;

public class FireblocksClient
{
    private readonly HttpClient _httpClient;

    public FireblocksClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(CreateTransactionResponse, ApiError)> CreateTransaction(CreateTransactionRequest request)
    {
        var jsonOptions = new JsonSerializerOptions{
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var httpResp = await _httpClient.PostAsJsonAsync("/v1/transactions", request, jsonOptions);

        if (httpResp.IsSuccessStatusCode)
        {
            var result = await httpResp.Content.ReadFromJsonAsync<CreateTransactionResponse>();
            return (result, null);
        }

        return (null, await BuildErrorResponse(httpResp));
    }

    private async Task<ApiError> BuildErrorResponse(HttpResponseMessage httpResp)
    {    
        ApiError apiError;

        try
        {
            apiError = await httpResp.Content.ReadFromJsonAsync<ApiError>();
        }
        catch
        {
            var content = await httpResp.Content.ReadAsStringAsync();
            apiError = new ApiError{
                Message = $"Http Response => Status: {httpResp.StatusCode}, Reason: {httpResp.ReasonPhrase}, Content: {content}"
            };
        }
        apiError.StatusCode = httpResp.StatusCode.ToString();

        return apiError;
    }
}