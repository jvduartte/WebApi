using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_C_.Model;
using WebApi_C_.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_C_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendaController : ControllerBase
     {

        private readonly VendaRepositorio _vendaRepo;

        public VendaController(VendaRepositorio vendaRepo)
        {
            _vendaRepo = vendaRepo;
        }




        [HttpGet("{id}/Comprovante")]
        public IActionResult GetFoto(int id)
        {
            // Busca o funcionário pelo ID
            var venda = _vendaRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (venda == null || venda.Comprovante == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(venda.Comprovante, "image/jpeg"); // Ou "image/png" dependendo do formato
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<List<Venda>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var vendas = _vendaRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (vendas == null || !vendas.Any())
            {
                return NotFound(new { Mensagem = "Nenhuma venda  encontrada." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = vendas.Select(venda => new Venda
            {
                Id = venda.Id,
                Valor = venda.Valor,   
                FkProduto = venda.FkProduto,
                FkCliente = venda.FkCliente,
                ComprovanteUrl = $"{Request.Scheme}://{Request.Host}/api/Venda/{venda.Id}/ComprovanteUrl"// Define a URL completa para a imagem

            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<Venda> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var venda = _vendaRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (venda == null)
            {
                return NotFound(new { Mensagem = "Venda  não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var vendaUrl = new Venda
            {
                Id = venda.Id,
                Valor = venda.Valor,
                FkProduto = venda.FkProduto,
                FkCliente = venda.FkCliente,
                ComprovanteUrl = $"{Request.Scheme}://{Request.Host}/api/Venda/{venda.Id}/ComprovanteUrl"// Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(vendaUrl);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] VendaDto novaVenda)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var venda = new Venda
            {
                Valor = novaVenda.Valor,
                FkProduto= novaVenda.FkProduto,
                FkCliente= novaVenda.FkCliente,

            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _vendaRepo.Add(venda, novaVenda.Comprovante);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Id =venda.Id,
                Mensagem = "Produto cadastrado com sucesso!",
                Valor = venda.Valor,
                FkProduto= venda.FkProduto,
                FkCliente= venda.FkCliente,
                
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] VendaDto vendaAtualizada)
        {
            // Busca o funcionário existente pelo Id
            var vendaExistente = _vendaRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            vendaExistente.Valor= vendaAtualizada.Valor;
            vendaExistente.FkProduto= vendaAtualizada.FkProduto;
            vendaExistente.FkCliente= vendaAtualizada.FkCliente;


            // Chama o método de atualização do repositório, passando a nova foto
            _vendaRepo.Update(vendaExistente, vendaAtualizada.Comprovante);

            // Cria a URL da foto
            var ComprovantelUrl = $"{Request.Scheme}://{Request.Host}/api/Venda/{vendaExistente.Id}/Comprovante";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Id = vendaExistente.Id,
                Mensagem = "Usuário atualizado com sucesso!",
                Valor = vendaExistente.Valor,
                FkProduto= vendaExistente.FkProduto,    
                FkCliente = vendaExistente.FkCliente,
                ComprovantelUrl = ComprovantelUrl // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var vendaExistente = _vendaRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Chama o método de exclusão do repositório
            _vendaRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {

                Id = vendaExistente.Id,
                Mensagem = "Venda excluída com sucesso!",
                Valor = vendaExistente.Valor,
                FkProduto = vendaExistente.FkProduto,
                FkCliente = vendaExistente.FkCliente,

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
