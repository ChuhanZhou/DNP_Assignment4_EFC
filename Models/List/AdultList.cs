using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.List
{
    public class AdultList
    {
        public List<Adult> adults { get; set; }

        public AdultList()
        {
            adults = new List<Adult>();
        }
        
        public AdultList(List<Adult> adults)
        {
            this.adults = new List<Adult>(adults);
        }

        public string AddAdult(Adult newAdult)
        {
            if (newAdult!=null)
            {
                if (adults.Any(Adult => Adult.Id==newAdult.Id))
                {
                    return "This id is used.";
                }
                adults.Add(newAdult);
                return null;
            }
            return "No this adult.";
        }

        public int GetCount()
        {
            return adults.Count;
        }

        public List<Adult> GetAllWithList()
        {
            var copy = new List<Adult>(adults);
            return copy;
        }

        public AdultList GetAdultListByFirstName(string firstName)
        {
            var choose = new AdultList();
            foreach (var adult in adults)
            {
                if (adult.FirstName.Equals(firstName))
                {
                    choose.AddAdult(adult);
                }
            }
            return choose;
        }
        
        public AdultList GetAdultListByLastName(string lastName)
        {
            var choose = new AdultList();
            foreach (var Adult in adults.Where(Adult => Adult.LastName.Equals(lastName)))
            {
                choose.AddAdult(Adult);
            }
            return choose;
        }
        
        public AdultList GetAdultListByHairColor(string hairColor)
        {
            var choose = new AdultList();
            foreach (var Adult in adults.Where(Adult => Adult.HairColor.Equals(hairColor)))
            {
                choose.AddAdult(Adult);
            }
            return choose;
        }
        
        public AdultList GetAdultListByEyeColor(string eyeColor)
        {
            var choose = new AdultList();
            foreach (var Adult in adults.Where(Adult => Adult.EyeColor.Equals(eyeColor)))
            {
                choose.AddAdult(Adult);
            }
            return choose;
        }
        
        public AdultList GetAdultListByAge(float min,float max)
        {
            var choose = new AdultList();
            foreach (var Adult in adults.Where(Adult => Adult.Age>=min&&Adult.Age<=max))
            {
                choose.AddAdult(Adult);
            }
            return choose;
        }
        
        public AdultList GetAdultListByWeight(float min,float max)
        {
            var choose = new AdultList();
            foreach (var Adult in adults.Where(Adult => Adult.Weight>=min&&Adult.Weight<=max))
            {
                choose.AddAdult(Adult);
            }
            return choose;
        }
        
        public AdultList GetAdultListByHeight(float min,float max)
        {
            var choose = new AdultList();
            foreach (var Adult in adults.Where(Adult => Adult.Height>=min&&Adult.Height<=max))
            {
                choose.AddAdult(Adult);
            }
            return choose;
        }
        public AdultList GetAdultListBySex(string sex)
        {
            var choose = new AdultList();
            foreach (var Adult in adults.Where(Adult => Adult.Sex.Equals(sex)))
            {
                choose.AddAdult(Adult);
            }
            return choose;
        }
        
        public Adult GetAdultById(int id)
        {
            return adults.FirstOrDefault(Adult => Adult.Id == id);
        }
        
        public Adult GetAdultByIndex(int index)
        {
            if (index<adults.Count&&index>=0)
            {
                return adults[index];
            }
            return null;
        }

        public void RemoveAdult(Person person)
        {
            for (int i = 0; i < adults.Count; i++)
            {
                if (adults[i].Id==person.Id)
                {
                    adults.RemoveAt(i);
                    break;
                }
            }
        }
        
        public void RemoveAdult(int index)
        {
            adults.RemoveAt(index);
        }
        public AdultList GetAllWithAdultList()
        {
            var copy = new AdultList();
            foreach (var Adult in adults)
            {
                copy.AddAdult(Adult.Copy());
            }
            return copy;
        }
        
        public AdultList GetAdultListByJob(string job)
        {
            var choose = new AdultList();
            foreach (var Adult in adults.Where(Adult => Adult.JobTitle.Equals(job)))
            {
                choose.AddAdult(Adult);
            }
            return choose;
        }

        public string UpdateAdult(Adult newAdult)
        {
            foreach (Adult adult in adults.Where(adult=>adult.Id==newAdult.Id))
            {
                adult.Update(newAdult);
                return null;
            }

            return "Can't find this adult.";
        }
    }
}