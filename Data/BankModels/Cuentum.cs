using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels;

public partial class Cuentum
{
    public int Id { get; set; }

    public int TipoCuentaId { get; set; }

    public int? ClienteId { get; set; }

    public decimal Saldo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual TipoCuentum TipoCuenta { get; set; } = null!;

    public virtual ICollection<TransaccionBancarium> TransaccionBancaria { get; set; } = new List<TransaccionBancarium>();
}
