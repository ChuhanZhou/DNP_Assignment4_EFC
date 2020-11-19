using System.Collections.Generic;

namespace DNP_Assignment4_EFC.Models.Unit  {
public class Child : Person {
    
    public List<Interest> ChildInterests { get; set; }
    public List<Pet> Pets { get; set; }

    public void Update(Child toUpdate) {
        base.Update(toUpdate);
        ChildInterests = toUpdate.ChildInterests;
        Pets = toUpdate.Pets;
    }

    public new Child Copy()
    {
        Child copy = new Child
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
            ChildInterests = new List<Interest>(),
            Pets = new List<Pet>()
        };
        if (ChildInterests!=null)
        {
            copy.ChildInterests = new List<Interest>(ChildInterests);
        }
        if (Pets!=null)
        {
            copy.Pets = new List<Pet>(Pets);
        }
        return copy;
    }
}
}