using ClinicService.Data;
using ClinicService.Services.Impl;
using ClinicService.Services.impl;
using ClinicService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using NLog.Web;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ClinicService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5001, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                    listenOptions.UseHttps(@"C:\Сертификаты\testcert.pfx", @"12345");
                });
            });





            builder.Services.AddDbContext<ClinicServiceDbContext>(options => 
            {
                options.UseSqlServer(builder.Configuration["DatabaseOptions:ConnectionString"]);
            });


            builder.Services.AddGrpc();

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });


            builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();

            builder.Services.AddScoped<IPetRepository, PetRepository>();
            builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();


            builder.Services.AddControllers();

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new
               TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticateService.SecretKey)),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ClockSkew = TimeSpan.Zero
               };
           });











            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();






            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseWhen( 
                ctx => ctx.Request.ContentType != "application/grpc",
                builder =>
                {
                    builder.UseHttpLogging();
                }
            );



            
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();


          
            app.UseEndpoints(endpoints =>  // 2
            {
                // Communication with gRPC endpoints must be made through a gRPC client.
                // To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909
                endpoints.MapGrpcService<ClientService>();  
                endpoints.MapGrpcService<ConsultationService>();
                endpoints.MapGrpcService<PetService>();
                endpoints.MapGrpcService<AuthService>();
            });

            app.Run();
        }
    }
}