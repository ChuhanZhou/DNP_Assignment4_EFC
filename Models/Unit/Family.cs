using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DNP_Assignment4_EFC.Models.DbUnit;
using DNP_Assignment4_EFC.Models.List;

namespace DNP_Assignment4_EFC.Models.Unit  {
public class Family {
    
    [Required]
    public string StreetName { get; set; }
    [Required]
    public int HouseNumber{ get; set; }
    public AdultList Adults { get; set; }
    public ChildList Children{ get; set; }
    public List<Pet> Pets{ get; set; }

    public Family() {
        Adults = new AdultList();
        Children = new ChildList();
        Pets = new List<Pet>();
    }

    public Family Copy()
    {
        Family copy = new Family
        {
            StreetName = StreetName,
            HouseNumber = HouseNumber,
            Adults = Adults.GetAllWithAdultList(),
            Children = Children.GetAllWithChildList(),
            Pets = new List<Pet>(Pets)
        };
        return copy;
    }

    public DbFamily ToDb()
    {
        List<DbAdultFamily> adultFamily = new List<DbAdultFamily>();
        foreach (var adult in Adults.adults)
        {
            adultFamily.Add(new DbAdultFamily
            {
                Address = StreetName + "[" + HouseNumber + "]",
                Id = adult.Id
            });
        }
        List<DbChildFamily> childFamily = new List<DbChildFamily>();
        foreach (var child in Children.childs)
        {
            childFamily.Add(new DbChildFamily()
            {
                Address = StreetName + "[" + HouseNumber + "]",
                Id = child.Id,
                //Child = child.ToDb()
            });
        }
        List<Pet> pets = new List<Pet>();
        if (Pets!=null)
        {
            pets = new List<Pet>(Pets);
            foreach (var pet in pets)
            {
                pet.PetId = StreetName + "[" + HouseNumber + "]" + "[" + pet.Id + "]";
            }
        }
        return new DbFamily
        {
            Address = StreetName + "[" + HouseNumber + "]",
            StreetName = StreetName,
            HouseNumber = HouseNumber,
            AdultFamily = adultFamily,
            ChildFamily = childFamily,
            Pets = new List<Pet>(pets)
        };
    }
}


}