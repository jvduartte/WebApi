using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_C_.Model;
using WebApi_C_.ORM;
using WebApi_C_.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_C_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _produtoRepo;

        public ProdutoController(ProdutoRepositorio produtoRepo)
        {
            _produtoRepo = produtoRepo;
        }


        [HttpGet("{id}/Nfiscal")]
        public IActionResult GetFoto(int id)
        {
            // Busca o funcionário pelo ID
            var produto = _produtoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produto == null || produto.Nfiscal == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(produto.Nfiscal, "image/jpeg"); // Ou "image/png" dependendo do formato
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var produtos = _produtoRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (produtos == null || !produtos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum produto encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = produtos.Select(produto => new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Qnt = produto.Qnt,
                NfiscalUrl = $"{Request.Scheme}://{Request.Host}/api/Produto/{produto.Id}/NfiscalUrl"// Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var produto = _produtoRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (produto == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var produtoUrl = new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Qnt = produto.Qnt,
                NfiscalUrl = $"{Request.Scheme}://{Request.Host}/api/Produto/{produto.Id}/NfiscalUrl"// Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(produtoUrl);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] ProdutoDto novoProduto)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var produto = new Produto
            {
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
                Qnt= novoProduto.Qnt,    
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _produtoRepo.Add(produto, novoProduto.Nfiscal);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto cadastrado com sucesso!",
                Nome = produto.Nome,
                Preco = produto.Preco,
                Qnt = produto.Qnt,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ProdutoDto produtoAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var produtoExistente = _produtoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Preco = produtoAtualizado.Preco;
            produtoExistente.Qnt = produtoAtualizado.Qnt;
           

            
            // Chama o método de atualização do repositório, passando a nova foto
            _produtoRepo.Update(produtoExistente, produtoAtualizado.Nfiscal);

            // Cria a URL da foto
            var NfiscalUrl = $"{Request.Scheme}://{Request.Host}/api/Produto/{produtoExistente.Id}/Nfiscal";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = produtoExistente.Nome,
                Preco=produtoExistente.Preco,
                Qnt = produtoExistente.Qnt,
                NfiscalUrl = NfiscalUrl // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var produtoExistente = _produtoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _produtoRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto excluído com sucesso!",
                Nome = produtoExistente.Nome,
                Preco=produtoExistente.Preco,
                Qnt=produtoExistente.Qnt,
        
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
