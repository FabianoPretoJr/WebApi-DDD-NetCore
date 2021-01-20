using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTO.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Municipio
{
    public class QuandoRequisitarMunicipio : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Municipio()
        {
            await AdicionarToken();

            var municipioDTO = new MunicipioCreateDTO
            {
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
            };

            // Post
            var response = await PostJsonASync(municipioDTO, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<MunicipioCreateResultDTO>(postResult);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(municipioDTO.Nome, registroPost.Nome);
            Assert.Equal(municipioDTO.CodIBGE, registroPost.CodIBGE);
            Assert.True(registroPost.Id != default(Guid));

            // Get All
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDTO>>(jsonResult);

            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(r => r.Id == registroPost.Id).Count() == 1);

            // Put
            var municipioUpdateDTO = new MunicipioUpdateDTO()
            {
                Id = registroPost.Id,
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(10000, 99999),
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(municipioUpdateDTO), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}municipios", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<MunicipioUpdateResultDTO>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(municipioUpdateDTO.Nome, registroAtualizado.Nome);
            Assert.Equal(municipioUpdateDTO.CodIBGE, registroAtualizado.CodIBGE);

            // Get Id
            response = await client.GetAsync($"{hostApi}municipios/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<MunicipioDTO>(jsonResult);

            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroAtualizado.Nome, registroSelecionado.Nome);
            Assert.Equal(registroAtualizado.CodIBGE, registroSelecionado.CodIBGE);

            // Get completeById
            response = await client.GetAsync($"{hostApi}municipios/completeById/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoCompleto = JsonConvert.DeserializeObject<MunicipioCompletoDTO>(jsonResult);

            Assert.NotNull(registroSelecionadoCompleto);
            Assert.Equal(registroAtualizado.Nome, registroSelecionadoCompleto.Nome);
            Assert.Equal(registroAtualizado.CodIBGE, registroSelecionadoCompleto.CodIBGE);
            Assert.NotNull(registroSelecionadoCompleto.Uf);
            Assert.Equal("São Paulo", registroSelecionadoCompleto.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoCompleto.Uf.Sigla);

            // Get completeByIBGE
            response = await client.GetAsync($"{hostApi}municipios/completeByIBGE/{registroAtualizado.CodIBGE}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            registroSelecionadoCompleto = JsonConvert.DeserializeObject<MunicipioCompletoDTO>(jsonResult);

            Assert.NotNull(registroSelecionadoCompleto);
            Assert.Equal(registroAtualizado.Nome, registroSelecionadoCompleto.Nome);
            Assert.Equal(registroAtualizado.CodIBGE, registroSelecionadoCompleto.CodIBGE);
            Assert.NotNull(registroSelecionadoCompleto.Uf);
            Assert.Equal("São Paulo", registroSelecionadoCompleto.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoCompleto.Uf.Sigla);

            // Delete
            response = await client.DeleteAsync($"{hostApi}municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Get Id depois de Delete
            response = await client.GetAsync($"{hostApi}municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}