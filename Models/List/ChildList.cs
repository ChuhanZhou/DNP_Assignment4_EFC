using System.Collections.Generic;
using System.Linq;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.List
{
    public class ChildList
    {
        public List<Child> childs { get; set; }

        public ChildList()
        {
            childs = new List<Child>();
        }

        public string AddChild(Child newChild)
        {
            if (newChild!=null)
            {
                if (childs.Any(Child => Child.Id==newChild.Id))
                {
                    return "This id is used.";
                }
                childs.Add(newChild);
                return null;
            }
            return "No this child.";
        }

        public int GetCount()
        {
            return childs.Count;
        }

        public List<Child> GetAllWithList()
        {
            var copy = new List<Child>(childs);
            return copy;
        }

        public ChildList GetChildListByFirstName(string firstName)
        {
            var choose = new ChildList();
            foreach (var Child in childs)
            {
                if (Child.FirstName.Equals(firstName))
                {
                    choose.AddChild(Child);
                }
            }
            return choose;
        }
        
        public ChildList GetChildListByLastName(string lastName)
        {
            var choose = new ChildList();
            foreach (var Child in childs.Where(Child => Child.LastName.Equals(lastName)))
            {
                choose.AddChild(Child);
            }
            return choose;
        }
        
        public ChildList GetChildListByHairColor(string hairColor)
        {
            var choose = new ChildList();
            foreach (var Child in childs.Where(Child => Child.HairColor.Equals(hairColor)))
            {
                choose.AddChild(Child);
            }
            return choose;
        }
        
        public ChildList GetChildListByEyeColor(string eyeColor)
        {
            var choose = new ChildList();
            foreach (var Child in childs.Where(Child => Child.EyeColor.Equals(eyeColor)))
            {
                choose.AddChild(Child);
            }
            return choose;
        }
        
        public ChildList GetChildListByAge(float min,float max)
        {
            var choose = new ChildList();
            foreach (var Child in childs.Where(Child => Child.Age>=min&&Child.Age<=max))
            {
                choose.AddChild(Child);
            }
            return choose;
        }
        
        public ChildList GetChildListByWeight(float min,float max)
        {
            var choose = new ChildList();
            foreach (var Child in childs.Where(Child => Child.Weight>=min&&Child.Weight<=max))
            {
                choose.AddChild(Child);
            }
            return choose;
        }
        
        public ChildList GetChildListByHeight(float min,float max)
        {
            var choose = new ChildList();
            foreach (var Child in childs.Where(Child => Child.Height>=min&&Child.Height<=max))
            {
                choose.AddChild(Child);
            }
            return choose;
        }
        public ChildList GetChildListBySex(string sex)
        {
            var choose = new ChildList();
            foreach (var Child in childs.Where(Child => Child.Sex.Equals(sex)))
            {
                choose.AddChild(Child);
            }
            return choose;
        }
        
        public Child GetChildById(int id)
        {
            return childs.FirstOrDefault(Child => Child.Id == id);
        }
        
        public Child GetChildByIndex(int index)
        {
            if (index<childs.Count&&index>=0)
            {
                return childs[index];
            }
            return null;
        }

        public void RemoveChild(Person person)
        {
            for (int i = 0; i < childs.Count; i++)
            {
                if (childs[i].Id==person.Id)
                {
                    childs.RemoveAt(i);
                    break;
                }
            }
        }
        
        public void RemoveChild(int index)
        {
            childs.RemoveAt(index);
        }
        public ChildList GetAllWithChildList()
        {
            var copy = new ChildList();
            foreach (var child in childs)
            {
                copy.AddChild(child.Copy());
            }
            return copy;
        }
        
        public ChildList GetChildListByInterest(string interest)
        {
            var choose = new ChildList();
            foreach (Child child in childs)
            {
                foreach (var childInterest in child.ChildInterests)
                {
                    if (childInterest.Type.Equals(interest))
                    { 
                        choose.AddChild(child);
                    }
                }
            }
            return choose;
        }

        public string UpdateChild(Child newChild)
        {
            foreach (Child child in childs.Where(child=>child.Id==newChild.Id))
            {
                child.Update(newChild);
                return null;
            }

            return "Can't find this child.";
        }
    }
}