using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using DNP_Assignment4_EFC.Models.DbUnit;

namespace DNP_Assignment4_EFC.Models.Unit {
public class Adult : Person {
    public static readonly List<string> ValidJob = new[]
    {
        "Teacher","Engineer","Garbage Collector","Shepherd","Pilot","Police Officer",
        "Pirate","Fireman","Astronaut","Captain","Soldier","Pizza Chef","Chef","Janitor",
        "Factory Worker","Chauffeur","Waitress","Ninja","Doctor","Nurse","Chemist","Caretaker",
        "Gardener","Mascot","Athlete","Entrepreneur","Archbishop"
    }.ToList();
    [ValidJob]
    public string JobTitle { get; set; }
    
    public IList<DbAdultFamily> AdultFamilies {get; set;}

    public override string ToString() {
        return JsonSerializer.Serialize(this);
    }

    public void Update(Adult toUpdate) {
        JobTitle = toUpdate.JobTitle;
        base.Update(toUpdate);
    }

    public new Adult Copy()
    {
        Adult copy = new Adult
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
            JobTitle = JobTitle
        };
        return copy;
    }
}

public class ValidJob : ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        List<string> valid = new List<string>(Adult.ValidJob);
        if (valid == null || valid.Contains((string)value)) 
        {
            return ValidationResult.Success;
        }
        //return new ValidationResult("Valid jobs are: " + JsonSerializer.Serialize(Adult.ValidJob));
        return new ValidationResult("Please choose a job.");
    }
}

}