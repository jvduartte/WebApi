using System;
using System.Collections.Generic;

namespace WebApi_C_.ORM;

public partial class TbProduto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Preco { get; set; } = null!;

    public int Qnt { get; set; }

    public byte[]? Nfiscal { get; set; }

    public virtual ICollection<TbVenda> TbVenda { get; set; } = new List<TbVenda>();
}
