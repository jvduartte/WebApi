using System.Text.Json.Serialization;

namespace WebApi_C_.Model
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Preco { get; set; } = null!;

        public int Qnt { get; set; }
        [JsonIgnore]
        public byte[]? Nfiscal { get; set; }
        [JsonIgnore] // Ignora a serialização deste campo
        public string? Base64Nfiscal => Nfiscal != null ? Convert.ToBase64String(Nfiscal) : null;

        public string NfiscalUrl { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
