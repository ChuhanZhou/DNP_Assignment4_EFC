using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using DNP_Assignment4_EFC.Models.DbUnit;

namespace DNP_Assignment4_EFC.Models.Unit  {
public class Interest {
    public static readonly List<string> ValidInterest = new[]
    {
        "Soccer", "Drawing", "Kite Flying", "Roller Blades",
        "Board Games", "Ballet", "Hockey", "Gaming", "Lego", 
        "Scout", "Gymnastics", "Harry Potter", "Frozen"
    }.ToList();
    
    [Key,ValidInterest]
    public string Type { get; set; }
    public IList<DbChildInterest> ChildInterest { get; set; }
}
public class ValidInterest : ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        List<string> valid = Interest.ValidInterest;
        if (valid == null || valid.Contains(value.ToString())) 
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("Valid interests are: " + JsonSerializer.Serialize(Interest.ValidInterest));
    }
}
}