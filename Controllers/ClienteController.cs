using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi_C_.Model;
using WebApi_C_.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_C_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {

        private readonly ClienteRepositorio _clienteRepo;

        public ClienteController(ClienteRepositorio clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        // GET: api/Funcionario/{id}/foto
        [HttpGet("{id}/DocIdentificacao")]
        public IActionResult GetFoto(int id)
        {
            // Busca o funcionário pelo ID
            var cliente = _clienteRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (cliente == null || cliente.DocIdentificacao == null)
            {
                return NotFound(new { Mensagem = "Doc não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(cliente.DocIdentificacao, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var clientes = _clienteRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (clientes == null || !clientes.Any())
            {
                return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = clientes.Select(cliente => new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                DocIdentificacaoUrl = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/DocIdentificacao"// Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }



         [HttpGet("{id}")]
          public ActionResult<Cliente> GetById(int id)
            {
                // Chama o repositório para obter o funcionário pelo ID
                var cliente = _clienteRepo.GetById(id);

                // Se o funcionário não for encontrado, retorna uma resposta 404
                if (cliente == null)
                {
                    return NotFound(new { Mensagem = "Cliente não encontrado." }); // Retorna 404 com mensagem
                }

                // Mapeia o funcionário encontrado para incluir a URL da foto
                var clienteUrl = new Cliente
                {
                    Id = cliente.Id,
                    Telefone = cliente.Telefone,
                    Nome = cliente.Nome,
                    DocIdentificacaoUrl = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/DocIdentificacao"// Define a URL completa para a imagem
                };

                // Retorna o funcionário com status 200 OK
                return Ok(clienteUrl);
            }


        [HttpPost]
        public ActionResult<object> Post([FromForm] ClienteDto novoCliente)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var cliente = new Cliente
            {
                Nome = novoCliente.Nome,
                Telefone = novoCliente.Telefone,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _clienteRepo.Add(cliente, novoCliente.DocIdentificacao);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Cliente cadastrado com sucesso!",
                aNome = cliente.Nome,
                aTelefone = cliente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }


        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ClienteDto clienteAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Telefone = clienteAtualizado.Telefone;

            // Chama o método de atualização do repositório, passando a nova foto
            _clienteRepo.Update(clienteExistente, clienteAtualizado.DocIdentificacao);

            // Cria a URL da foto
            var DocIdentificacaoUrl = $"{Request.Scheme}://{Request.Host}/api/Cliente/{clienteExistente.Id}/DocIdentificacao";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                aNome = clienteExistente.Nome,
                aTelefone = clienteExistente.Telefone,
                UDocIdentificacaoUrl = DocIdentificacaoUrl // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }


        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _clienteRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                aNome = clienteExistente.Nome,
                aTelefone = clienteExistente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

    }
} 

