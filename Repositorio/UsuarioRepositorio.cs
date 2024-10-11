using Microsoft.EntityFrameworkCore;
using WebApi_C_.Model;
using WebApi_C_.ORM;

namespace  WebApi_C_.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly WebApiVitorContext _context;

        public UsuarioRepositorio (WebApiVitorContext context)
        {
            _context = context;
        }

        public TbUsuario GetByCredentials(string usuario, string senha)
        {
            // Aqui você deve usar a lógica de hash para comparar a senha
            return _context.TbUsuarios.FirstOrDefault(u => u.Usuario == usuario && u.Senha == senha);
        }

        // Você pode adicionar métodos adicionais para gerenciar usuários
    }
}
