using System.ComponentModel.DataAnnotations;

namespace Mission6_Simmons.Models;

public class Categories
{
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<movie> Movies { get; set; }
}