using System.Text.Json.Serialization;

namespace WebApi_C_.Model
{
    public class Venda
    {
        public int Id { get; set; }

        public string Valor { get; set; } = null!;
        [JsonIgnore]
        public byte[]? Comprovante { get; set; }

        public int FkProduto { get; set; }

        public int FkCliente { get; set; }
        [JsonIgnore]
        public string? Base64Comprovante => Comprovante != null ? Convert.ToBase64String(Comprovante) : null;

        public string ComprovanteUrl { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
