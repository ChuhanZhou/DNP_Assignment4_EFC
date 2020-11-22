using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.DbUnit
{
    public class DbChild
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        public string Sex { get; set; }
        public List<Pet> Pets { get; set; }
        public IList<DbChildFamily> ChildFamily {get; set;}
        public IList<DbChildInterest> ChildInterest {get; set;}

        public void Update(DbChild child)
        {
            Id = child.Id;
            FirstName = child.FirstName;
            LastName = child.LastName;
            HairColor = child.HairColor;
            EyeColor = child.EyeColor;
            Age = child.Age;
            Weight = child.Weight;
            Height = child.Height;
            Sex = child.Sex;
            Pets = child.Pets;
            ChildFamily = child.ChildFamily;
            ChildInterest = child.ChildInterest;
        }
        
        public Child ToChild()
        {
            List<Interest> interests = new List<Interest>();
            foreach (var childInterest in ChildInterest)
            {
                interests.Add(childInterest.Interest);
            }
            List<Pet> pets = new List<Pet>();
            if (Pets!=null)
            {
                pets = Pets;
            }
            return new Child
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
                ChildInterests = interests,
                Pets = new List<Pet>(pets)
            };
        }
    }
}