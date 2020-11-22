using System;
using System.Collections.Generic;
using DNP_Assignment4_EFC.Models.DbUnit;

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

    public DbChild ToDb()
    {
        List<DbChildInterest> childInterest = new List<DbChildInterest>();
        foreach (var interest in ChildInterests)
        {
            childInterest.Add(new DbChildInterest
            {
                Id = Id,
                Type = interest.Type,
                //Interest = interest
            });
        }
        List<Pet> pets = new List<Pet>();
        if (Pets!=null)
        {
            pets = Pets;
        }
        return new DbChild
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
            ChildInterest = childInterest,
            Pets = new List<Pet>(pets)
        };
    }
}
}