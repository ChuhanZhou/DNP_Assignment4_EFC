using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using DNP_Assignment4_EFC.Models.DbUnit;

namespace DNP_Assignment4_EFC.Models.Unit  {
public class Person {
    
    public static readonly List<string> ValidHairColor = new[]
    {
        "Blond", "Red", "Brown", "Black",
        "White", "Grey", "Blue", "Green", "Leverpostej"
    }.ToList();
    
    public static readonly List<string> ValidEyeColor = new[]
    {
        "Brown", "Grey", "Green", "Blue",
        "Amber", "Hazel"
    }.ToList();
    
    public static readonly List<string> ValidSex = new[]
    {
        "M", "F"
    }.ToList();

    [Key]
    public int Id { get; set; }
    [NotNull]
    public string FirstName { get; set; }
    [NotNull]
    public string LastName { get; set; }
    [ValidHairColor]
    public string HairColor { get; set; }
    [NotNull]
    [ValidEyeColor]
    public string EyeColor { get; set; }
    [NotNull, Range(0, 125)]
    public int Age { get; set; }
    [NotNull, Range(1, 250)]
    public float Weight { get; set; }
    [NotNull, Range(30, 250)]
    public int Height { get; set; }
    [NotNull]
    [ValidSex]
    public string Sex { get; set; }

    public void Update(Person toUpdate) {
        Age = toUpdate.Age;
        Height = toUpdate.Height;
        HairColor = toUpdate.HairColor;
        Sex = toUpdate.Sex;
        Weight = toUpdate.Weight;
        EyeColor = toUpdate.EyeColor;
        FirstName = toUpdate.FirstName;
        LastName = toUpdate.LastName;
    }

    public Person Copy()
    {
        Person copy = new Person
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            HairColor = HairColor,
            EyeColor = EyeColor,
            Age = Age,
            Weight = Weight,
            Height = Height,
            Sex = Sex,
        };
        return copy;
    }

}

public class ValidHairColor : ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        List<string> valid = Person.ValidHairColor;
        if (valid == null || valid.Contains((string)value)) {
            return ValidationResult.Success;
        }
        //return new ValidationResult("Valid hair colors are: " + JsonSerializer.Serialize(Person.ValidHairColor));
        return new ValidationResult("Please choose a hair color.");
    }
}

public class ValidEyeColor : ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        List<string> valid = Person.ValidEyeColor;
        if (valid != null && valid.Contains((string)value)) {
            return ValidationResult.Success;
        }
        //return new ValidationResult("Valid eye colors are: " + JsonSerializer.Serialize(Person.ValidEyeColor));
        return new ValidationResult("Please choose a eye color.");
    }
}

public class ValidSex : ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        List<string> valid = Person.ValidSex;
        if (valid != null && valid.Contains((string)value)) {
            return ValidationResult.Success;
        }
        return new ValidationResult("Valid sex are: " + JsonSerializer.Serialize(Person.ValidSex));
    }
}
}