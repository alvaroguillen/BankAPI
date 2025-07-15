using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels;

public partial class TransaccionBancarium
{
    public int Id { get; set; }

    public int CuentaId { get; set; }

    public int TipoTransaccionId { get; set; }

    public decimal Monto { get; set; }

    public int? CuentaExterna { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Cuentum Cuenta { get; set; } = null!;

    public virtual TipoTransaccion TipoTransaccion { get; set; } = null!;
}
