using System;
using System.Collections.Generic;
using Api.Domain.DTO.Uf;

namespace Api.Service.Test.Uf
{
    public class UfTestes
    {
        public static string Nome { get; set; }
        public static string Sigla { get; set; }
        public static Guid UfId { get; set; }

        public List<UfDTO> listaUfDTO = new List<UfDTO>();
        public UfDTO ufDTO;

        public UfTestes()
        {
            UfId = Guid.NewGuid();
            Sigla = Faker.Address.UsState().Substring(1, 3);
            Nome = Faker.Address.UsState();

            for (int i = 0; i < 10; i++)
            {
                var item = new UfDTO()
                {
                    Id = Guid.NewGuid(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    Nome = Faker.Address.UsState()
                };
                listaUfDTO.Add(item);
            }

            ufDTO = new UfDTO
            {
                Id = UfId,
                Sigla = Sigla,
                Nome = Nome
            };
        }
    }
}