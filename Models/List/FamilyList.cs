using System.Collections.Generic;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.List
{
    public class FamilyList
    {
        public List<Family> families { get; set; }

        public FamilyList()
        {
            families = new List<Family>();
        }

        public string AddFamily(Family newFamily)
        {
            foreach (var family in families)
            {
                if (family.HouseNumber==newFamily.HouseNumber&&family.StreetName==newFamily.StreetName)
                {
                    return "This address is used.";
                }
            }
            foreach (var pet in newFamily.Pets)
            {
                pet.PetId = newFamily.ToDb().Address + "[" + pet.Id + "]";
            }
            families.Add(newFamily);
            return null;
        }

        public int GetCount()
        {
            return families.Count;
        }
        
        public FamilyList GetAllWithFamilyList()
        {
            FamilyList copy = new FamilyList();
            foreach (var family in families)
            {
                copy.AddFamily(family.Copy());
            }
            return copy;
        }
        
        public List<Family> GetAllWithList()
        {
            List<Family> copy = new List<Family>(families);
            return copy;
        }

        public FamilyList GetFamilyListByStreetName(string streetName)
        {
            var choose = new FamilyList();
            foreach (var family in families)
            {
                if (family.StreetName.Equals(streetName))
                {
                    choose.AddFamily(family);
                }
            }
            return choose;
        }

        public FamilyList GetFamilyListByHouseNumber(int houseNumber)
        {
            var choose = new FamilyList();
            foreach (var family in families)
            {
                if (family.HouseNumber.Equals(houseNumber))
                {
                    choose.AddFamily(family);
                }
            }
            return choose;
        }
        
        public FamilyList GetFamilyListByPerson(Person person)
        {
            var choose = new FamilyList();
            foreach (var family in families)
            {
                if (family.Adults.GetAdultById(person.Id)!=null)
                {
                    choose.AddFamily(family);
                }
                else if (family.Children.GetChildById(person.Id)!=null)
                {
                    choose.AddFamily(family);
                }
            }
            return choose;
        }
        
        public Family GetFamilyByStreetNameAndHouseNumber(string streetName, int houseNumber)
        {
            return GetFamilyListByStreetName(streetName).GetFamilyListByHouseNumber(houseNumber).GetFamilyByIndex(0);
        }
        
        public Family GetFamilyByIndex(int index)
        {
            if (index<families.Count&&index>=0)
            {
                return families[index];
            }
            return null;
        }

        public string UpdateFamily(Family oldFamily, Family newFamily)
        {
            if (!oldFamily.HouseNumber.Equals(newFamily.HouseNumber)||!oldFamily.StreetName.Equals(newFamily.StreetName))
            {
                if (GetFamilyByStreetNameAndHouseNumber(newFamily.StreetName,newFamily.HouseNumber)!=null)
                {
                    return "The new address is used.";
                }  
            }
            
            for (int i = 0; i < families.Count; i++)
            {
                if (families[i].HouseNumber.Equals(oldFamily.HouseNumber)&&families[i].StreetName.Equals(oldFamily.StreetName))
                {
                    foreach (var pet in newFamily.Pets)
                    {
                        pet.PetId = newFamily.ToDb().Address + "[" + pet.Id + "]";
                    }
                    families[i] = newFamily;
                    return null;
                }
            }
            return "Can't find the target family.";
        }

        public void RemoveFamily(Family family)
        {
            for (int i = 0; i < families.Count; i++)
            {
                if (families[i].HouseNumber.Equals(family.HouseNumber)&&families[i].StreetName.Equals(family.StreetName))
                {
                    families.RemoveAt(i);
                }
            }
        }
    }
}