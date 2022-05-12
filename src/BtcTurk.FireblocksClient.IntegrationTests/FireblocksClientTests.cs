using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using BtcTurk.FireblocksClient.Types;

namespace BtcTurk.FireblocksClient.IntegrationTests;

public class FireblocksClientTests
{
    public FireblocksClient _fireblocksClient;

    [SetUp]
    public void Setup()
    {
        var fbApiKey = Environment.GetEnvironmentVariable("FB_API_KEY");
        var fbApiPrivateKey = Environment.GetEnvironmentVariable("FB_API_PK");

        if (string.IsNullOrWhiteSpace(fbApiKey) || string.IsNullOrWhiteSpace(fbApiPrivateKey))
        {
            throw new Exception("Api key or private key env variable is missing");
        }

        var services = new ServiceCollection();
        services.AddFireblocksClient(fbApiKey, fbApiPrivateKey);
        var sp = services.BuildServiceProvider();

        _fireblocksClient = sp.GetRequiredService<FireblocksClient>();
    }

    [Test]
    public async Task Test_Create_Transaction_Is_Successful()
    {
        var request = new CreateTransactionRequest {
             AssetId = "ETH_TEST",
            Amount = "0.1",
            Source = new TransferSource {
                Type = "VAULT_ACCOUNT",
                Id = "2"
            },
            Destination = new TransferDestination {
                Type = "EXTERNAL_WALLET",
                Id = "cfad6f65-0eeb-f6ee-8895-39b49c6a83e6"
            },
            Note = "Created by custom dotnet code trial"
        };

        var (result, err) = await _fireblocksClient.CreateTransaction(request);

        Assert.Null(err);
        Assert.NotNull(result);
        Assert.DoesNotThrow( () => System.Guid.Parse(result.Id));
        Assert.AreEqual(result.Status, "SUBMITTED");
    }
}