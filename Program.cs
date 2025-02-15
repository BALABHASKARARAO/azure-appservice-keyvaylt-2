using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
SecretClientOptions options = new SecretClientOptions()
    {
        Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
    };
var client = new SecretClient(new Uri("https://my-keyvaultbala.vault.azure.net/"), new DefaultAzureCredential(),options);

KeyVaultSecret secret = client.GetSecret("<mySecret>");

string secretValue = secret.Value;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => secretValue);

app.Run();