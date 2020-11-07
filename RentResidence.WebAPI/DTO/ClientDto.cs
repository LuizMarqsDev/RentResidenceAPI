namespace RentResidence.WebAPI.DTO
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string Sexo { get; set; }     
        public string Telefone { get; set; }
        public int ResidenceId { get; set; }
    }
}
