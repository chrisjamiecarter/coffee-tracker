using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTracker.Api.Models;

[Table("CoffeeRecord")]
public class CoffeeRecord
{
    #region Properties

    [Key]
    public required Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [DataType(DataType.Date), Required]
    public required DateTime Date { get; set; }

    #endregion
}
