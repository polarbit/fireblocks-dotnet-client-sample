namespace BtcTurk.FireblocksClient.Types;

public class ApiError {
    public string Message { get; set; }

    public string Code { get; set; }

    public string StatusCode { get; set; }
}

public class CreateTransactionRequest
{
    public string AssetId { get; set; }

    public TransferSource Source { get; set; }

    public TransferDestination Destination { get; set; }

    public string Amount { get; set; }

    public string Note { get; set; }
}

public class CreateTransactionResponse
{
    public string Id { get; set; }

    public string Status { get; set; }
}

public class TransferSource
{
    public string Id { get; set; }

    public string Type { get; set; }
}

public class TransferDestination
{
    public string Id { get; set; }

    public string Type { get; set; }
}