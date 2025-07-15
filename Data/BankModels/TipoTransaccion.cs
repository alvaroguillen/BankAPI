using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels;

public partial class TipoTransaccion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<TransaccionBancarium> TransaccionBancaria { get; set; } = new List<TransaccionBancarium>();
}
