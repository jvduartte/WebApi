using System;
using System.Collections.Generic;

namespace WebApi_C_.ORM;

public partial class TbVenda
{
    public int Id { get; set; }

    public string Valor { get; set; } = null!;

    public byte[]? Comprovante { get; set; }

    public int FkProduto { get; set; }

    public int FkCliente { get; set; }

    public virtual TbCliente FkClienteNavigation { get; set; } = null!;

    public virtual TbProduto FkProdutoNavigation { get; set; } = null!;
}
