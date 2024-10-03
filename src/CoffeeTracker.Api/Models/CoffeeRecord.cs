using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTracker.Api.Models;

[Table("CoffeeRecord")]
public class CoffeeRecord
{
    #region Properties

    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [DataType(DataType.Date), Required]
    public DateTime Date { get; set; }

    #endregion
}
