using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentResidence.Domain
{
    public class Client
    {

        public int ClientId { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required]
        [StringLength(70)]
        public string Email { get; set; }

        [Required]
        [StringLength(250)]
        public string NomeCompleto { get; set; }

        [Required]
        [StringLength(20)]
        public string Sexo { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefone { get; set; }
        public int ResidenceId { get; set; }
        public Residence Residence { get; }
    }
}
