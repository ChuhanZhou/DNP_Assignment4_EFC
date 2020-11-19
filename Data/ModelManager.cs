using DNP_Assignment4_EFC.Data;
using DNP_Assignment4_EFC.Models;
using DNP_Assignment4_EFC.Models.List;
using DNP_Assignment4_EFC.Models.Unit;
using DNP_Assignment4_EFC.Persistence;

namespace DNP_Assignment4_EFC.Data
{
    public class ModelManager : IModelManager
    {
        private static ModelManager modelManager;
        private ModelPackage modelPackage;
        private readonly string dataFileName;

        public static ModelManager GetModelManager()
        {
            if (modelManager == null)
            {
                modelManager = new ModelManager();
            }
            return modelManager;
        }
        
        public ModelManager()
        {
            modelPackage = new ModelPackage();
            dataFileName = "DataFile.json";
            ReadData();
        }

        private void ReadData()
        {
            DataFileContext.ReadData(dataFileName,modelPackage);
        }

        private void UpdateData()
        {
            DataFileContext.UpdateData(dataFileName,modelPackage);
        }

        public string AddUser(User newUser)
        {
            string result = modelPackage.UserList.AddUser(newUser);
            UpdateData();
            return result;
        }

        public bool Login(User user)
        {
            return modelPackage.UserList.CheckPassword(user);
        }

        public UserList GetAllUser()
        {
            return modelPackage.UserList.GetAllUser();
        }

        public string UpdatePassword(User oldUser,User newUser)
        {
            string result = modelPackage.UserList.UpdatePassword(oldUser,newUser);
            UpdateData();
            return result;
        }

        public void RemoveUser(User user)
        {
            modelPackage.UserList.RemoveUser(user);
            UpdateData();
        }

        public string AddFamily(Family newFamily)
        {
            string result = modelPackage.FamilyList.AddFamily(newFamily);
            UpdateData();
            return result;
        }

        public FamilyList GetAllFamily()
        {
            return modelPackage.FamilyList.GetAllWithFamilyList();
        }

        public string UpdateFamily(Family oldFamily, Family newFamily)
        {
            string result = modelPackage.FamilyList.UpdateFamily(oldFamily, newFamily);
            UpdateData();
            return result;
        }

        public void RemoveFamily(Family family)
        {
            modelPackage.FamilyList.RemoveFamily(family);
            UpdateData();
        }

        public string AddAdult(Adult newAdult)
        {
            if (modelPackage.ChildList.GetChildById(newAdult.Id)==null)
            {
                string result = modelPackage.AdultList.AddAdult(newAdult);
                UpdateData();
                return result;
            }
            return "This id is used.";
        }

        public AdultList GetAllAdult()
        {
            return modelPackage.AdultList.GetAllWithAdultList();
        }

        public string AddChild(Child newChild)
        {
            if (modelPackage.AdultList.GetAdultById(newChild.Id)==null)
            {
                string result = modelPackage.ChildList.AddChild(newChild);
                UpdateData();
                return result;
            }
            return "This id is used.";
        }

        public ChildList GetAllChild()
        {
            return modelPackage.ChildList.GetAllWithChildList();
        }

        public string UpdatePerson(Person newPerson)
        {
            string result = null;
            if (modelPackage.AdultList.GetAdultById(newPerson.Id)!=null)
            {
                result = modelPackage.AdultList.UpdateAdult((Adult) newPerson);
                if (result==null)
                {
                    foreach (var family in modelPackage.FamilyList.GetFamilyListByPerson(newPerson).families)
                    {
                        family.Adults.UpdateAdult((Adult) newPerson);
                    }
                }
            }
            else if (modelPackage.ChildList.GetChildById(newPerson.Id)!=null)
            {
                result = modelPackage.ChildList.UpdateChild((Child) newPerson);
                if (result==null)
                {
                    foreach (var family in modelPackage.FamilyList.GetFamilyListByPerson(newPerson).families)
                    {
                        family.Children.UpdateChild((Child) newPerson);
                    }
                }
            }
            UpdateData();
            return result;
        }

        public void RemovePerson(Person person)
        {
            modelPackage.AdultList.RemoveAdult(person);
            modelPackage.ChildList.RemoveChild(person);
            foreach (var family in modelPackage.FamilyList.families)
            {
                family.Adults.RemoveAdult(person);
                family.Children.RemoveChild(person);
            }
            UpdateData();
        }
    }
}