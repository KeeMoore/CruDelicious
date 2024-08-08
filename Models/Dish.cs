#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CruDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Chef's Name")]
        public string ChefName { get; set; }

        [Required]
        [Range(1, 5)]
        public int Tastiness { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0")]
        public int Calories { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
    public class DishModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // Other properties
}

}
