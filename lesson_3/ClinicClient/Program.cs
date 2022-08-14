
using Grpc.Net.Client;
using System.Reflection.Metadata;
using static ClientServiceProtos.ClientService;

AppContext.SetSwitch(
              "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
using var channel = GrpcChannel.ForAddress("http://localhost:5001");

ClientServiceClient client = new ClientServiceClient(channel);

var createClientResponse = client.CreateClient(new ClientServiceProtos.CreateClientRequest
{
    Document = "*****",
    FirstName = "*****",
    Surname = "*******",
    Patronymic = "*******"
});

Console.WriteLine($"Client ({createClientResponse.ClientId}) created successfully.");


var getClientsResponse = client.GetClients(new ClientServiceProtos.GetClientsRequest());
if (getClientsResponse.ErrCode == 0)
{
    Console.WriteLine("Clients:");
    Console.WriteLine("========\n");
    foreach (var clientDto in getClientsResponse.Clients)
    {
        Console.WriteLine($"({clientDto.ClientId}/{clientDto.Document}) {clientDto.Surname} {clientDto.FirstName} {clientDto.Patronymic}");
    }
}

Console.ReadKey();