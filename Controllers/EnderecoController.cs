using Microsoft.AspNetCore.Mvc;
using WebApi_C_.Model;
using WebApi_C_.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_C_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoRepositorio _enderecoRepo;

        public EnderecoController(EnderecoRepositorio enderecoRepo)
        {
            _enderecoRepo = enderecoRepo;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<List<Endereco>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var endereco = _enderecoRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (endereco == null || !endereco.Any())
            {
                return NotFound(new { Mensagem = "Nenhum endereço encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = endereco.Select(endereco => new Endereco
            {
                Id = endereco.Id,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                N = endereco.N,
                FkCliente = endereco.FkCliente,
                Preferencia = endereco.Preferencia,


            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var endereco = _enderecoRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (endereco == null)
            {
                return NotFound(new { Mensagem = "Endereco não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var enderecoUrl = new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                N = endereco.N,
                FkCliente = endereco.FkCliente,
                Preferencia = endereco.Preferencia,

            };

            // Retorna o funcionário com status 200 OK
            return Ok(enderecoUrl);
        }


        // POST api/<ValuesController>
        [HttpPost] 
        public ActionResult<object> Post([FromForm] EnderecoDto novoEndereco)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var endereco = new Endereco
            {
                Logradouro = novoEndereco.Logradouro,
                Cidade= novoEndereco.Cidade,    
                Estado= novoEndereco.Estado,    
                Cep= novoEndereco.Cep,
                N = novoEndereco.N,
                FkCliente = novoEndereco.FkCliente,
                Preferencia = novoEndereco.Preferencia,

            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _enderecoRepo.Add(endereco);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereco cadastrado com sucesso!",
                Logradouro= endereco.Logradouro,
                Cidade= endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                N = endereco.N,
                FkCliente= endereco.FkCliente,
                Preferencia= endereco.Preferencia,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EnderecoDto enderecoAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            enderecoExistente.Logradouro = enderecoAtualizado.Logradouro;
            enderecoExistente.Cidade = enderecoAtualizado.Cidade;
            enderecoExistente.Estado = enderecoAtualizado.Estado;
            enderecoExistente.N = enderecoAtualizado.N; 
            enderecoExistente.FkCliente = enderecoAtualizado.FkCliente;
            enderecoExistente.Preferencia = enderecoAtualizado.Preferencia;

            // Chama o método de atualização do repositório, passando a nova foto
            _enderecoRepo.Update(enderecoExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço atualizado com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                N = enderecoExistente.N,
                FkCliente = enderecoExistente.FkCliente,
                Preferencia = enderecoExistente.Preferencia,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }


        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _enderecoRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                N = enderecoExistente.N,
                FkCliente = enderecoExistente.FkCliente,
                Preferencia = enderecoExistente.Preferencia,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

    }
}
