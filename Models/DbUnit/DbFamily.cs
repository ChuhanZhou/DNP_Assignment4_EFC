using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DNP_Assignment4_EFC.Models.List;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.DbUnit
{
    public class DbFamily
    {
        [Key]
        public string Address { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public List<Pet> Pets{ get; set; }
        public IList<DbAdultFamily> AdultFamily { get; set; }
        public IList<DbChildFamily> ChildFamily { get; set; }
        public Family ToFamily()
        {
            List<Adult> adults = new List<Adult>();
            if (AdultFamily!=null)
            {
                foreach (var adultFamily in AdultFamily)
                {
                    adults.Add(adultFamily.Adult);
                }
            }
            List<Child> children = new List<Child>();
            if (ChildFamily!=null)
            {
                foreach (var childFamily in ChildFamily)
                {
                    children.Add(childFamily.Child.ToChild());
                }
            }
            List<Pet> pets = new List<Pet>();
            if (Pets!=null)
            {
                pets = Pets;
            }
            return new Family
            {
                StreetName = StreetName,
                HouseNumber = HouseNumber,
                Adults = new AdultList(adults),
                Children = new ChildList(children),
                Pets = new List<Pet>(pets)
            };
        }
    }
}