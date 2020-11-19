using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DNP_Assignment4_EFC.Models.Unit  {
public class Pet {
    public static readonly List<string> ValidSpecies = new[]
    {
        "Hamster", "Bunny", "Frog", "Budgerigar",
        "Owl", "Snake","Dog","Cat"
    }.ToList();
    public int Id { get; set; }
    [Required]
    [ValidSpecies]
    public string Species { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Age { get; set; }
}

public class ValidSpecies : ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        List<string> valid = Pet.ValidSpecies;
        if (valid == null || valid.Contains((string)value)) {
            return ValidationResult.Success;
        }
        return new ValidationResult("Please choose a Species.");
    }
}
}