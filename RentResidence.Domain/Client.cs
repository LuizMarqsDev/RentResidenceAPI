﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentResidence.Domain
{
    public class Client
    {

        public int ClienteId { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required]
        [StringLength(10)]
        public DateTime DataNascimento { get; set; }

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
        public virtual Residence Residence { get; set; }
    }
}