using System.Text.Json.Serialization;

namespace WebApi_C_.Model
{
    public class VendaDto
    {
        public string Valor { get; set; } = null!;
        [JsonIgnore]
        public IFormFile Comprovante { get; set; }

        public int FkProduto { get; set; }

        public int FkCliente { get; set; }
        
    }
}
