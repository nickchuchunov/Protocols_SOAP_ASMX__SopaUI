using ClientServiceProtos;
using ClinicService.Data;
using Grpc.Core;
using static ClientServiceProtos.ClientService;

namespace ClinicService.Services.Impl
{
    public class ClientService : ClientServiceBase
    {
        #region Serives

        private readonly ClinicServiceDbContext _dbContext;
        private readonly ILogger<ClientService> _logger;
       

        #endregion

        #region Constructors

        public ClientService(ClinicServiceDbContext dbContext,
            ILogger<ClientService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;

            
        }

        #endregion

        public override Task<ClientServiceProtos.CreateClientResponse> CreateClient(ClientServiceProtos.CreateClientRequest request, ServerCallContext context)
        {
            var client = new Client
            {
                Document = request.Document,
                Surname = request.Surname,
                FirstName = request.FirstName,
                Patronymic = request.Patronymic
            };
            _dbContext.Clients.Add(client);

            _dbContext.SaveChanges();

            var response = new CreateClientResponse
            {
                ClientId = client.ClientId
            };

            return Task.FromResult(response);
        }

        public override Task<GetClientsResponse> GetClients(GetClientsRequest request, ServerCallContext context)
        {
            var response = new GetClientsResponse();
            response.Clients.AddRange(_dbContext.Clients.Select(client => new ClientResponse
            {
                ClientId = client.ClientId,
                Document = client.Document,
                FirstName = client.FirstName,
                Patronymic = client.Patronymic,
                Surname = client.Surname
            }).ToList());

            return Task.FromResult(response);
        }

        public override Task <ClientServiceProtos.UpdateClientResponse> UpdateClient (ClientServiceProtos.UpdateClientRequest request, ServerCallContext context)
        {

            var response = new UpdateClientResponse();

            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == request.ClientId) ;

            if (client==null)
            {
                response.ErrCode = 1;
                response.ErrMessage = "клиенгт не найден в базе данных";
            }

            client.Document = request.Document;
            client.Surname = request.Surname;
            client.FirstName = request.FirstName;
            client.Patronymic = request.Patronymic;

            _dbContext.Update(client);
            _dbContext.SaveChanges();
            return Task.FromResult(response);

        }





        public override Task<ClientServiceProtos.DeleteClientResponse> DeleteClient(ClientServiceProtos.DeleteClientRequest request, ServerCallContext context) 
        {

          var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == request.ClientId);
            var response = new DeleteClientResponse();

            if (client == null)
            {
                response.ErrCode = 1;
                response.ErrMessage = "клиенгт не найден в базе данных";
            }

            _dbContext.Remove(client);
            _dbContext.SaveChanges();

            return Task.FromResult(response);


        }



        public override Task<ClientServiceProtos.ClientResponse> GetClientById(ClientServiceProtos.GetClientByIdRequest request, ServerCallContext context)
        {


            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == request.ClientId);
            var response = new ClientResponse();

            if (client != null)
            {
                response.ClientId = client.ClientId;
                response.Document = client.Document;
                response.Surname = client.Surname;
                response.FirstName = client.FirstName;
                response.FirstName = client.FirstName;
                response.Patronymic = client.Patronymic;
            }
            else
            {
                response.ClientId = default;
                response.Document = String.Empty;
                response.Surname = String.Empty;
                response.FirstName = String.Empty;
                response.FirstName = String.Empty;
                response.Patronymic = String.Empty;
            }
            return Task.FromResult(response);

        }








    }
}
