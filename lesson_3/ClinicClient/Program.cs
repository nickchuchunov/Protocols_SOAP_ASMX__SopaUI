
using Grpc.Core;
using Grpc.Net.Client;
using System.Reflection.Metadata;
using static ClientServiceProtos.ClientService;

//AppContext.SetSwitch(
//              "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
using var channel = GrpcChannel.ForAddress("https://localhost:5001");

ClinicService.Protos.AuthenticateService.AuthenticateServiceClient authenticateServiceClient = new ClinicService.Protos.AuthenticateService.AuthenticateServiceClient(channel);
var authenticationResponse = authenticateServiceClient.Login(new ClinicService.Protos.AuthenticationRequest
{
    UserName = "sample@gmail.com",
    Password = "12345"
});

if (authenticationResponse.Status != 0)
{
    Console.WriteLine("Authentication error.");
    Console.ReadKey();
    return;
}

Console.WriteLine($"Session token: {authenticationResponse.SessionInfo.SessionToken}");



var credentials = CallCredentials.FromInterceptor((c, m) =>
{
    m.Add("Authorization",
        $"Bearer {authenticationResponse.SessionInfo.SessionToken}");
    return Task.CompletedTask;
});

var protectedChannel = GrpcChannel.ForAddress("https://localhost:5001",
        new GrpcChannelOptions
        {

            Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
        });



ClientServiceClient client = new ClientServiceClient(protectedChannel);


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