using System.Collections.Generic;
using System.Linq;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.List
{
    public class PersonList
    {
        public List<Person> persons { get; set; }

        public PersonList()
        {
            persons = new List<Person>();
        }

        public string AddPerson(Person newPerson)
        {
            if (newPerson!=null)
            {
                if (persons.Any(person => person.Id==newPerson.Id))
                {
                    return "This id is used.";
                }
                persons.Add(newPerson);
                return null;
            }
            return "Input null.";
        }

        public int GetCount()
        {
            return persons.Count;
        }
        
        public PersonList GetAllWithPersonList()
        {
            var copy = new PersonList();
            foreach (var person in persons)
            {
                copy.AddPerson(person.Copy());
            }
            return copy;
        }

        public List<Person> GetAllWithList()
        {
            var copy = new List<Person>(persons);
            return copy;
        }

        public PersonList GetPersonListByFirstName(string firstName)
        {
            var choose = new PersonList();
            foreach (var person in persons)
            {
                if (person.FirstName.Equals(firstName))
                {
                    choose.AddPerson(person);
                }
            }
            return choose;
        }
        
        public PersonList GetPersonListByLastName(string lastName)
        {
            var choose = new PersonList();
            foreach (var person in persons.Where(person => person.LastName.Equals(lastName)))
            {
                choose.AddPerson(person);
            }
            return choose;
        }
        
        public PersonList GetPersonListByHairColor(string hairColor)
        {
            var choose = new PersonList();
            foreach (var person in persons.Where(person => person.HairColor.Equals(hairColor)))
            {
                choose.AddPerson(person);
            }
            return choose;
        }
        
        public PersonList GetPersonListByEyeColor(string eyeColor)
        {
            var choose = new PersonList();
            foreach (var person in persons.Where(person => person.EyeColor.Equals(eyeColor)))
            {
                choose.AddPerson(person);
            }
            return choose;
        }
        
        public PersonList GetPersonListByAge(float min,float max)
        {
            var choose = new PersonList();
            foreach (var person in persons.Where(person => person.Age>=min&&person.Age<=max))
            {
                choose.AddPerson(person);
            }
            return choose;
        }
        
        public PersonList GetPersonListByWeight(float min,float max)
        {
            var choose = new PersonList();
            foreach (var person in persons.Where(person => person.Weight>=min&&person.Weight<=max))
            {
                choose.AddPerson(person);
            }
            return choose;
        }
        
        public PersonList GetPersonListByHeight(float min,float max)
        {
            var choose = new PersonList();
            foreach (var person in persons.Where(person => person.Height>=min&&person.Height<=max))
            {
                choose.AddPerson(person);
            }
            return choose;
        }
        public PersonList GetPersonListBySex(string sex)
        {
            var choose = new PersonList();
            foreach (var person in persons.Where(person => person.Sex.Equals(sex)))
            {
                choose.AddPerson(person);
            }
            return choose;
        }
        
        public Person GetPersonById(int id)
        {
            return persons.FirstOrDefault(person => person.Id == id);
        }
        
        public Person GetPersonByIndex(int index)
        {
            if (index<persons.Count&&index>=0)
            {
                return persons[index];
            }
            return null;
        }

        public string UpdatePerson(Person newPerson)
        {
            if (newPerson!=null)
            {
                foreach (var person in persons.Where(person => person.Id==newPerson.Id))
                {
                    person.Update(newPerson);
                    return null;
                }
                return "Can't find this person.";
            }
            return "Input null.";
        }

        public void RemovePerson(Person person)
        {
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].Id==person.Id)
                {
                    persons.RemoveAt(i);
                    break;
                }
            }
        }
        
        public void RemovePerson(int index)
        {
            persons.RemoveAt(index);
        }
    }
}