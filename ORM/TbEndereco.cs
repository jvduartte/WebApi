using System;
using System.Collections.Generic;

namespace WebApi_C_.ORM;

public partial class TbEndereco
{
    public int Id { get; set; }

    public string Logradouro { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public string Preferencia { get; set; } = null!;

    public int N { get; set; }

    public int FkCliente { get; set; }

    public virtual TbCliente FkClienteNavigation { get; set; } = null!;
}
