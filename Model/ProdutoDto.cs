using WebApi_C_.ORM;

namespace WebApi_C_.Model
{
    public class ProdutoDto
    {
        public string Nome { get; set; } = null!;

        public string Preco { get; set; } = null!;

        public int Qnt { get; set; }

        public IFormFile Nfiscal { get; set; }
    
    }
}
