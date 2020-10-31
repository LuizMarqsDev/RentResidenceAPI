using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentResidence.Domain
{
    public class Residence
    {
        public int ResidenceId { get; set; }

        [Required]
        [StringLength(70)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(20)]
        public string CEP { get; set; }

        [Required]
        [StringLength(70)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(20)]
        public string Complemento { get; set; }

        [Required]
        [StringLength(70)]
        public string Estado { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [StringLength(70)]
        public string Rua { get; set; }

    }
}
