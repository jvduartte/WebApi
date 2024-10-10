using System.Text.Json.Serialization;

namespace WebApi_C_.Model
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Telefone { get; set; } = null!;
        [JsonIgnore]
        public byte[]? DocIdentificacao { get; set; }
        
        [JsonIgnore] // Ignora a serialização deste campo
        public string? Base64DocIdentificacao => DocIdentificacao != null ? Convert.ToBase64String(DocIdentificacao) : null;

        public string DocIdentificacaoUrl { get; set; } // Certifique-se de que esta propriedade esteja visível
    }

}

