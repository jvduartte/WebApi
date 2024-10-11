using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WebApi_C_.Model;
using WebApi_C_.ORM;




namespace WebApi_C_.Repositorio
{
    public class VendaRepositorio
    {
        private readonly WebApiVitorContext _context;

        public VendaRepositorio(WebApiVitorContext context)
        {
            _context = context;
        }
        

        public void Add(Venda venda, IFormFile Comprovante)
        {
            // Verifica se uma foto foi enviada
            byte[] ComprovanteBytes = null;
            if (Comprovante != null && Comprovante.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    Comprovante.CopyTo(memoryStream);
                    ComprovanteBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var TbVenda = new TbVenda()
            {
                
                Valor = venda.Valor,
                Comprovante = venda.Comprovante,
                FkProduto = venda.FkProduto,
                FkCliente = venda.FkCliente,

            };

            // Adiciona a entidade ao contexto
            _context.Add(TbVenda);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVendas.FirstOrDefault(v => v.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Remove a entidade do contexto
                _context.TbVendas.Remove(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrado.");
            }
        }


        public List<Venda> GetAll()
        {
            List<Venda> listFun = new List<Venda>();

            var listTb = _context.TbVendas.ToList();

            foreach (var item in listTb)
            {
                var venda = new Venda
                {
                    Id = item.Id,
                    Valor = item.Valor,
                    Comprovante= item.Comprovante,
                    FkProduto= item.FkProduto,
                    FkCliente= item.FkCliente,
                };

                listFun.Add(venda);
            }

            return listFun;
        }


        public Venda GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbVendas.FirstOrDefault(v => v.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var venda = new Venda
            {
                Id = item.Id,
                Valor = item.Valor,
                Comprovante = item.Comprovante,
                FkProduto = item.FkProduto, 
                FkCliente = item.FkCliente,
                
            };

            return venda; // Retorna o funcionário encontrado
        }


        public void Update (Venda venda, IFormFile Comprovante)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVendas.FirstOrDefault(v => v.Id == venda.Id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbVenda.Valor = venda.Valor;
                tbVenda.Comprovante = venda.Comprovante;
                tbVenda.FkProduto = venda.FkProduto;
                tbVenda.FkCliente = venda.FkCliente;

                // Verifica se uma nova foto foi enviada
                if (Comprovante != null && Comprovante.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        Comprovante.CopyTo(memoryStream);
                        tbVenda.Comprovante = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbVendas.Update(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrado.");
            }
        }

        
    }
}
