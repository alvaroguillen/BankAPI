using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Data.BankModels;

public partial class Client
{
    public int Id { get; set; }

    [MaxLength(200, ErrorMessage ="El nombre debe ser menor a 200 caracteres.")]
    public string Name { get; set; } = null!;

    [MaxLength(40, ErrorMessage = "El numero de telefono debe ser menor a 40 caracteres.")]
    public string PhoneNumber { get; set; } = null!;

    [EmailAddress(ErrorMessage ="El formato de correo es incorrecto.")]
    [MaxLength(50, ErrorMessage = "El email debe contener menos de 50 caracteres.")]
    public string? Email { get; set; }

    public DateTime RegDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
