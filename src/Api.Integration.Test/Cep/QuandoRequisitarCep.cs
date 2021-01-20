using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTO.Cep;
using Api.Domain.DTO.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Cep
{
    public class QuandoRequisitarCep : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Cep()
        {
            await AdicionarToken();

            var municipioCreateDto = new MunicipioCreateDTO()
            {
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
            };

            var response = await PostJsonASync(municipioCreateDto, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<MunicipioCreateResultDTO>(postResult);

            var cepCreateDTO = new CepCreateDTO
            {
                Cep = Faker.RandomNumber.Next(10000, 99999).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 2000).ToString(),
                MunicipioId = registroPost.Id
            };

            // Post
            response = await PostJsonASync(cepCreateDTO, $"{hostApi}ceps", client);
            postResult = await response.Content.ReadAsStringAsync();
            var registroPostCep = JsonConvert.DeserializeObject<CepCreateResultDTO>(postResult);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(cepCreateDTO.Cep, registroPostCep.Cep);
            Assert.Equal(cepCreateDTO.Logradouro, registroPostCep.Logradouro);
            Assert.Equal(cepCreateDTO.Numero, registroPostCep.Numero);
            Assert.Equal(cepCreateDTO.MunicipioId, registroPostCep.MunicipioId);
            Assert.True(registroPostCep.Id != default(Guid));

            // Get Id
            response = await client.GetAsync($"{hostApi}ceps/{registroPostCep.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<CepDTO>(jsonResult);

            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroPostCep.Id, registroSelecionado.Id);
            Assert.Equal(registroPostCep.Cep, registroSelecionado.Cep);
            Assert.Equal(registroPostCep.Logradouro, registroSelecionado.Logradouro);
            Assert.Equal(registroPostCep.Numero, registroSelecionado.Numero);
            Assert.Equal(registroPostCep.MunicipioId, registroSelecionado.MunicipioId);

            // Get Cep
            response = await client.GetAsync($"{hostApi}ceps/byCep/{registroPostCep.Cep}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            registroSelecionado = JsonConvert.DeserializeObject<CepDTO>(jsonResult);

            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroPostCep.Id, registroSelecionado.Id);
            Assert.Equal(registroPostCep.Cep, registroSelecionado.Cep);
            Assert.Equal(registroPostCep.Logradouro, registroSelecionado.Logradouro);
            Assert.Equal(registroPostCep.Numero, registroSelecionado.Numero);
            Assert.Equal(registroPostCep.MunicipioId, registroSelecionado.MunicipioId);

            // Put
            var cepUpdateDTO = new CepUpdateDTO()
            {
                Id = registroPostCep.Id,
                Cep = Faker.RandomNumber.Next(10000, 99999).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 2000).ToString(),
                MunicipioId = registroPost.Id
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(cepUpdateDTO), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}ceps", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<CepUpdateResultDTO>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(cepUpdateDTO.Cep, registroAtualizado.Cep);
            Assert.Equal(cepUpdateDTO.Logradouro, registroAtualizado.Logradouro);
            Assert.Equal(cepUpdateDTO.Numero, registroAtualizado.Numero);

            // Delete
            response = await client.DeleteAsync($"{hostApi}ceps/{registroPostCep.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Get Id depois de Delete
            response = await client.GetAsync($"{hostApi}ceps/{registroPostCep.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}