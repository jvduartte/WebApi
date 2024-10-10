using System;
using System.Collections.Generic;

namespace WebApi_C_.ORM;

public partial class TbCliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public byte[]? DocIdentificacao { get; set; }

    public virtual ICollection<TbEndereco> TbEnderecos { get; set; } = new List<TbEndereco>();

    public virtual ICollection<TbVenda> TbVenda { get; set; } = new List<TbVenda>();
}
