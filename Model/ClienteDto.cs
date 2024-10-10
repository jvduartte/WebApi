namespace WebApi_C_.Model
{
    public class ClienteDto
    {

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public IFormFile DocIdentificacao { get; set; } // Campo para receber a foto
    }
}
