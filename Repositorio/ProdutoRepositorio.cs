using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WebApi_C_.Model;
using WebApi_C_.ORM;




namespace WebApi_C_.Repositorio
{
    public class ProdutoRepositorio
    {
        private readonly WebApiVitorContext _context;

        public ProdutoRepositorio(WebApiVitorContext context)
        {
            _context = context;
        }




        public void Add(Produto produto, IFormFile Nfiscal)
        {
            byte[] NfiscalBytes = null;
            if (Nfiscal != null && Nfiscal.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    Nfiscal.CopyTo(memoryStream);
                    NfiscalBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbProdutos = new TbProduto()
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Qnt = produto.Qnt,
                Nfiscal = NfiscalBytes,
                
            
            };

            // Adiciona a entidade ao contexto
            _context.TbProdutos.Add(tbProdutos);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProduto = _context.TbProdutos.FirstOrDefault(p => p.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbProduto != null)
            {
                // Remove a entidade do contexto
                _context.TbProdutos.Remove(tbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }


        public List<Produto> GetAll()
        {
            List<Produto> listFun = new List<Produto>();

            var listTb = _context.TbProdutos.ToList();

            foreach (var item in listTb)
            {
                var produto = new Produto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Preco = item.Preco,
                    Qnt = item.Qnt,
                    Nfiscal = item.Nfiscal,
                };

                listFun.Add(produto);
            }

            return listFun;
        }


        public Produto GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbProdutos.FirstOrDefault(p => p.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var produto = new Produto
            {
                Id = item.Id,
                Nome = item.Nome,
                Preco = item.Preco,
                Qnt = item.Qnt,
                Nfiscal = item.Nfiscal,
            };

            return produto; // Retorna o funcionário encontrado
        }


        public void Update (Produto produto, IFormFile Nfiscal)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProduto = _context.TbProdutos.FirstOrDefault(p => p.Id == produto.Id);

            // Verifica se a entidade foi encontrada
            if (tbProduto != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbProduto.Nome = produto.Nome;
                tbProduto.Preco = produto.Preco;
                tbProduto.Qnt = produto.Qnt;
                tbProduto.Nfiscal = produto.Nfiscal;

                // Verifica se uma nova foto foi enviada
                if (Nfiscal != null && Nfiscal.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        Nfiscal.CopyTo(memoryStream);
                        tbProduto.Nfiscal = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbProdutos.Update(tbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }

        
    }
}
