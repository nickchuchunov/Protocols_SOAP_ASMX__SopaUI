
using ClientServiceProtos;
using ClinicService.Data;
using Grpc.Core;
using static ClientServiceProtos.ClientService;
using ClinicService.Data;



namespace ClinicService.Services.impl
{
    public class PetService : ClientServiceBase
    {
        private readonly ClinicServiceDbContext _dbContext;
        private readonly ILogger<PetService> _logger;

        PetService(ClinicServiceDbContext dbContext, ILogger<PetService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;

        }


        public override Task<ClientServiceProtos.PetAddResponse> PetAdd(PetAddRequest request, ServerCallContext context) 
        {
            Pet pet = new Pet();
            PetAddResponse response = new PetAddResponse();

            pet.ClientId = request.ClientId;
            pet.Name = request.Name;
            pet.Birthday = request.Birthday.ToDateTime();

          var  _pet = _dbContext.Pets.FirstOrDefault(pet => pet.ClientId == request.ClientId);

            if (_pet.Name!= pet.Name&& pet.Name!=null&& pet.ClientId!=null)
            {
                _dbContext.Pets.Add(pet);
                _dbContext.SaveChanges();
            } 
            else if(_pet.Name == pet.Name)
            {
                response.ErrCode = 1;
                response.ErrMessage = " Животное с таким именем уже существует ";
            }
            else if (pet.Name == null)
            {
                response.ErrCode = 2;
                response.ErrMessage += " Не указано имя животного";
            }

            else if (pet.ClientId == null)
            {

                response.ErrCode = 3;
                response.ErrMessage += " Не указан номер клиента";




            }


            return Task.FromResult(response);


        }


        public override Task<ClientServiceProtos.PetDeleteResponse> PetDelete(PetDeleteRequest request, ServerCallContext context)
        {

            var pet = _dbContext.Pets.FirstOrDefault(pet => pet.PetId == request.PetId);

            PetDeleteResponse response = new PetDeleteResponse();

            if (pet == null)
            {
                response.ErrCode = 1; response.ErrMessage = "В базе данных нет животных по указанному ID";

            }
            else 
            {
                _dbContext.Remove(pet);
                _dbContext.SaveChanges();
            }

            return Task.FromResult(response);



        }


        public override Task<ClientServiceProtos.PetGetAllResponse> PetGetAll(PetGetAllRequest request, ServerCallContext context) 
        { 
        List<Pet> PetList = _dbContext.Pets.ToList();

        PetGetAllResponse response = new PetGetAllResponse();

            PetResponse petResponse = new PetResponse();


         if (PetList== null) 
            {
                response.ErrCode = 1; response.ErrMessage = "В базе данных нет значений";
            }
            else 
            {

                for (int i = 0; i < PetList.Count; i++)
                {
                    petResponse.PetId = PetList[i].PetId;
                    petResponse.ClientId = PetList[i].ClientId;
                    petResponse.Name = PetList[i].Name;
                    TimeSpan time = new TimeSpan(PetList[i].Birthday.Day, PetList[i].Birthday.Hour, PetList[i].Birthday.Minute, PetList[i].Birthday.Second);
                    petResponse.Birthday.ToDateTime().Add(time);


                    response.Pet.Add(petResponse);
                   

                };


            };
            return Task.FromResult(response);

        }


        public override Task<ClientServiceProtos.PetGetByIdResponse> PetGetById(PetGetByIdRequest request, ServerCallContext context)
        {
         var pet = _dbContext.Pets.FirstOrDefault(pet => pet.PetId == request.PetId);

         PetGetByIdResponse response = new PetGetByIdResponse();

            response.PetId = pet.PetId;
            response.ClientId = pet.ClientId;
            response.Name = pet.Name;

            TimeSpan time = new TimeSpan(pet.Birthday.Day, pet.Birthday.Hour, pet.Birthday.Minute, pet.Birthday.Second);
            response.Birthday.ToDateTime().Add(time) ;

            return Task.FromResult(response);

        }



        public override Task<ClientServiceProtos.PetUpdateResponse> PetUpdate(PetUpdateRequest request, ServerCallContext context)
        {
            var pet = _dbContext.Pets.FirstOrDefault(pet => pet.PetId == request.PetId);
            PetUpdateResponse response = new PetUpdateResponse();

            if (pet == null)
            {
                response.ErrCode = 1; response.ErrMessage = "Указанное животное отсутсвует в базе данных";


            }
            else
            {
                Pet pet1 = new Pet();
                pet1.PetId = request.PetId;
                pet1.Name = request.Name;
                pet1.Birthday = request.Birthday.ToDateTime();

                _dbContext.Pets.Update(pet1);
                _dbContext.SaveChanges();

            }

            return Task.FromResult(response);

        }










    }
}
