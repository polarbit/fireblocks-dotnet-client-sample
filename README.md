This project is just a example for creating and signing JWT token for **Fireblocks** in **dotnet**; and initiating a transaction as a starting point. 

For a ready to use solution have a look at this project: 
> https://github.com/trakx/fireblocks-api-client

### Notes

* Reads API_KEY and PRIVATE_KEY from environment variables
* Private key format (PEM)
```
-----BEGIN PRIVATE KEY-----
MIIJQQIBADANBgkqhkiG9w0BAQEFAASCCSswggknAgEAAoICAQC6NxBtkX8e8gIU
IEF2mo0uVv257Q89SgH8Oh1cr6gAOdkUJI746X7yjzulQNaBeOvcSh5vva3kFxjf
...
...
zGc/O8mwY4hHC/cXcVWL7tOVINwhl/TFnzM6tk0SsHye4YH9LQekIYD7qyPEhsEW
bBKMlnelJf+F6bow6jArIYg3CUpe7cbfZgjWFrU9ksjgtabouzZDrkDj45FvHn1d
4HrAChzwnbky8YQq2XgHAFic6F/E
-----END PRIVATE KEY-----
```