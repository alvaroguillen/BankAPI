using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Correo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
