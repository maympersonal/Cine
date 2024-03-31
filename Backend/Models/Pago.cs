using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models;

[Table("Pago")]
public partial class Pago
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
    [Column("IdPg")]
    public int IdPg { get; set; }

    //[InverseProperty("IdPgNavigation")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    
    //[InverseProperty("IdPgNavigation")]
    public virtual Efectivo? Efectivo { get; set; }
    
    //[InverseProperty("IdPgNavigation")]
    public virtual Punto? Punto { get; set; }

  
    //[InverseProperty("IdPgNavigation")]
    public virtual Web? Web { get; set; }
}
