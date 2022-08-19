using ClientServiceProtos;
using ClinicService.Data;
using Grpc.Core;
using static ClientServiceProtos.ClientService;
using ClinicService.Data;




namespace ClinicService.Services.impl
{
    public class ConsultationService : ClientServiceBase
    {
       private readonly ClinicServiceDbContext _dbContext;
       private readonly ILogger<ConsultationService> _logger;
      
       ConsultationService(ClinicServiceDbContext dbContext, ILogger<ConsultationService> logger)
       {
           _logger = logger;
           _dbContext = dbContext;
      
       }
      
      
       public override Task<ClientServiceProtos.ConsultationAddResponse> ConsultationAdd(ClientServiceProtos.ConsultationAddRequest request, ServerCallContext context) 
       {
           ConsultationAddResponse response = new ConsultationAddResponse();
      
           var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == request.ClientId);
      
          var pet = _dbContext.Pets.FirstOrDefault(pet => pet.PetId == request.PetId);
      
           if (client==null) { response.ErrCode = 1; response.ErrMessage = " Клиент не найден в базе данных "; }
      
           if (pet == null) { response.ErrCode = 2; response.ErrMessage += " Животное не найдено в базе данных "; }
      
           Consultation сonsultation = new Consultation();
      
           сonsultation.ClientId = request.ClientId;
           сonsultation.PetId = request.ClientId;
           сonsultation.ConsultationDate = request.ConsultationDate.ToDateTime();
           сonsultation.Description = request.Description;
           сonsultation.Client = client;
           сonsultation.Pet = pet;
      
           _dbContext.Consultations.Add(сonsultation);
           _dbContext.SaveChanges();
      
           return Task.FromResult(response);
      
      
      
       }
      
      
       public override Task<ClientServiceProtos.ConsultationDeleteResponse> ConsultationDelete(ConsultationDeleteRequest request, ServerCallContext context)
       {
           var consultation = _dbContext.Consultations.FirstOrDefault(consultations => consultations.ConsultationId == request.ConsultationId);
      
           ConsultationDeleteResponse response = new ConsultationDeleteResponse();
           if (consultation==null) { response.ErrCode = 1; response.ErrMessage = "Клиент не найден в базе данных";  }
      
           _dbContext.Remove(consultation);
           _dbContext.SaveChanges();
      
           return Task.FromResult(response);
      
      
       }
      
       public override Task<ClientServiceProtos.ConsultationGetAllResponse> ConsultationGetAll(ConsultationGetAllRequest request, ServerCallContext context)
       {
          var  consultation = _dbContext.Consultations.ToList();
           ConsultationGetAllResponse response = new ConsultationGetAllResponse();
           if (consultation == null) { response.ErrCode = 1; response.ErrMessage = "база с консультациями пуста";  }

            
            ConsultationResponse response1 = new ConsultationResponse();

            for (int i=0; i< consultation.Count; i++) 
            {
                response1.Consultationid = consultation[i].ConsultationId;
                response1.ClientId = consultation[i].ClientId;
                response1.PetId = consultation[i].PetId;
                TimeSpan time = new TimeSpan(consultation[i].ConsultationDate.Day, consultation[i].ConsultationDate.Hour, consultation[i].ConsultationDate.Minute, consultation[i].ConsultationDate.Second);
                response1.ConsultationDate.ToDateTime().Add(time);
                response1.Description = consultation[i].Description;

                response.Consultation.Add(response1);
            }




           
      
           return Task.FromResult(response);
      
       }
      
      
       public override Task<ClientServiceProtos.ConsultationGetByIdResponse> ConsultationGetById(ConsultationGetByIdRequest request, ServerCallContext context)
       {
           ConsultationGetByIdResponse response = new ConsultationGetByIdResponse();
      
          var consultation = _dbContext.Consultations.FirstOrDefault(consultations => consultations.ConsultationId == request.ConsultationId);
      
           response.ConsultationId = consultation.ConsultationId;
           response.ClientId = consultation.ClientId;
           response.PetId = consultation.PetId;
            TimeSpan time = new TimeSpan(consultation.ConsultationDate.Day, consultation.ConsultationDate.Hour, consultation.ConsultationDate.Minute, consultation.ConsultationDate.Second);
            response.ConsultationDate.ToDateTime().Add(time);
           response.Description = consultation.Description;
      
           return Task.FromResult(response);
       }
      
       public override Task<ClientServiceProtos.ConsultationUpdateResponse> ConsultationUpdate( ConsultationUpdateRequest request, ServerCallContext context)
       {
           ConsultationUpdateResponse response = new ConsultationUpdateResponse();
      
           
           var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == request.ClientId);
      
      
           var pet = _dbContext.Pets.FirstOrDefault(pet => pet.PetId == request.PetId);
      
           if (client==null) { response.ErrCode = 1; response.ErrMessage = "Клиент не найден в базе данных "; }
           if (pet==null) { response.ErrCode = 2; response.ErrMessage += " Животное не найдено в базе данных"; }
      
           Consultation consultation = new Consultation();
      
           consultation.ConsultationId = request.ConsultationId;
           consultation.ClientId = request.ClientId;
           consultation.PetId = request.PetId;

           consultation.ConsultationDate = request.ConsultationDate.ToDateTime();
           consultation.Description = request.Description;
           consultation.Client = client;
           consultation.Pet = pet;
      
           _dbContext.Update(consultation);
           _dbContext.SaveChanges();
      
           return Task.FromResult(response);
       }
      


    }
}
