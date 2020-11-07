using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentResidence.WebAPI.DTO
{
    public class ResidenceDto
    {
        public int ResidenceId { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public string Estado { get; set; }
        public int Numero { get; set; }
        public string Rua { get; set; }
        public ClientDto Client { get; private set; }

    }
}
