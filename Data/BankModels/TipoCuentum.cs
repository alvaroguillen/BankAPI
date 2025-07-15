using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels;

public partial class TipoCuentum
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
