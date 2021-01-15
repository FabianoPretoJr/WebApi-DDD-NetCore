using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Api.CrossCutting.Mappings;
using Api.Data.Data;
using Api.Domain.DTO;
using application;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public ApplicationDbContext database { get; private set; }
        public HttpClient client { get; private set; }
        public IMapper mapper { get; set; }
        public string hostApi { get; set; }
        public HttpResponseMessage response { get; set; }

        public BaseIntegration()
        {
            hostApi = "http://localhost:5000/api/";
            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();
            var server = new TestServer(builder);

            database = server.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            database.Database.Migrate();

            mapper = new AutoMapperFixture().GetMapper();

            client = server.CreateClient();
        }

        public async Task AdicionarToken()
        {
            var loginDTO = new LoginDTO()
            {
                Email = "admin@mail.com"
            };

            var resultLogin = await PostJsonASync(loginDTO, $"{hostApi}login", client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResponse>(jsonLogin);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginObject.accessToken);
        }

        public static async Task<HttpResponseMessage> PostJsonASync(object dataclass, string url, HttpClient client)
        {
            return await client.PostAsync(
                url, 
                new StringContent(JsonConvert.SerializeObject(dataclass), 
                System.Text.Encoding.UTF8, 
                "application/json")
            );
        }

        public void Dispose()
        {
            database.Dispose();
            client.Dispose();
        }
    }

    public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddProfile(new ModelToEntityProfile());
                    cfg.AddProfile(new DtoToModelProfile());
                    cfg.AddProfile(new EntityToDtoProfile());
                });

                return config.CreateMapper();
            }

            public void Dispose()
            {
                
            }
        }
}