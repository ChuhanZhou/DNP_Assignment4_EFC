using System;
using System.Threading.Tasks;
using DNP_Assignment4_EFC.DataAccess;
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
            //UpdateDatabase();
        }

        private async Task UpdateDatabase()
        {
            DataFileContext.ReadData(dataFileName,modelPackage);
            Console.WriteLine(0);
            foreach (var user in modelPackage.UserList.Users)
            {
                Console.WriteLine(1);
                await DatabaseModelManager.AddUser(user);
            }
            foreach (var adult in modelPackage.AdultList.adults)
            {
                Console.WriteLine(2);
                await DatabaseModelManager.AddAdult(adult);
            }
            await DatabaseModelManager.SetAllInterests();
            foreach (var child in modelPackage.ChildList.childs)
            {
                Console.WriteLine(3);
                await DatabaseModelManager.AddChild(child);
            }
            foreach (var family in modelPackage.FamilyList.families)
            {
                Console.WriteLine(4);
                await DatabaseModelManager.AddFamily(family);
            }
        }
        
        private async Task ReadData()
        {
            modelPackage.UserList = await DatabaseModelManager.GetAllUser();
            modelPackage.AdultList = await DatabaseModelManager.GetAllAdult();
            modelPackage.ChildList = await DatabaseModelManager.GetAllChild();
            modelPackage.FamilyList = await DatabaseModelManager.GetAllFamily();
        }

        private void UpdateData()
        {
            DataFileContext.UpdateData(dataFileName,modelPackage);
        }

        public async Task<string> AddUser(User newUser)
        {
            string result = modelPackage.UserList.AddUser(newUser);
            if (result==null)
            {
                await DatabaseModelManager.AddUser(modelPackage.UserList.GetUserByUserName(newUser.UserName));
            }
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

        public async Task<string> UpdatePassword(User oldUser,User newUser)
        {
            string result = modelPackage.UserList.UpdatePassword(oldUser,newUser);
            if (result==null)
            {
                await DatabaseModelManager.UpdateUser(modelPackage.UserList.GetUserByUserName(newUser.UserName));
            }
            UpdateData();
            return result;
        }

        public async Task RemoveUser(User user)
        {
            modelPackage.UserList.RemoveUser(user);
            await DatabaseModelManager.RemoveUser(user);
            UpdateData();
        }

        public async Task<string> AddFamily(Family newFamily)
        {
            string result = modelPackage.FamilyList.AddFamily(newFamily);
            if (result==null)
            {
                await DatabaseModelManager.AddFamily(newFamily);
            }
            UpdateData();
            return result;
        }

        public FamilyList GetAllFamily()
        {
            return modelPackage.FamilyList.GetAllWithFamilyList();
        }

        public async Task<string> UpdateFamily(Family oldFamily, Family newFamily)
        {
            string result = modelPackage.FamilyList.UpdateFamily(oldFamily, newFamily);
            if (result == null)
            {
                await DatabaseModelManager.UpdateFamily(newFamily);
            }
            UpdateData();
            return result;
        }

        public async Task RemoveFamily(Family family)
        {
            modelPackage.FamilyList.RemoveFamily(family);
            await DatabaseModelManager.RemoveFamily(family);
            UpdateData();
        }

        public async Task<string> AddAdult(Adult newAdult)
        {
            if (modelPackage.ChildList.GetChildById(newAdult.Id)==null)
            {
                string result = modelPackage.AdultList.AddAdult(newAdult);
                if (result==null)
                {
                    await DatabaseModelManager.AddAdult(newAdult);
                }
                UpdateData();
                return result;
            }
            return "This id is used.";
        }

        public AdultList GetAllAdult()
        {
            return modelPackage.AdultList.GetAllWithAdultList();
        }

        public async Task<string> AddChild(Child newChild)
        {
            if (modelPackage.AdultList.GetAdultById(newChild.Id)==null)
            {
                string result = modelPackage.ChildList.AddChild(newChild);
                if (result==null)
                {
                    await DatabaseModelManager.AddChild(newChild);
                }
                UpdateData();
                return result;
            }
            return "This id is used.";
        }

        public ChildList GetAllChild()
        {
            return modelPackage.ChildList.GetAllWithChildList();
        }

        public async Task<string> UpdatePerson(Person newPerson)
        {
            string result = null;
            if (modelPackage.AdultList.GetAdultById(newPerson.Id)!=null)
            {
                result = modelPackage.AdultList.UpdateAdult((Adult) newPerson);
                if (result==null)
                {
                    await DatabaseModelManager.UpdateAdult((Adult) newPerson);
                    foreach (var family in modelPackage.FamilyList.GetFamilyListByPerson(newPerson).families)
                    {
                        family.Adults.UpdateAdult((Adult) newPerson);
                        //await DatabaseModelManager.UpdateFamily(family);
                    }
                }
            }
            else if (modelPackage.ChildList.GetChildById(newPerson.Id)!=null)
            {
                result = modelPackage.ChildList.UpdateChild((Child) newPerson);
                if (result==null)
                {
                    Console.WriteLine(1);
                    await DatabaseModelManager.UpdateChild((Child) newPerson);
                    foreach (var family in modelPackage.FamilyList.GetFamilyListByPerson(newPerson).families)
                    {
                        family.Children.UpdateChild((Child) newPerson);
                        //await DatabaseModelManager.UpdateFamily(family);
                    }
                }
            }
            UpdateData();
            return result;
        }

        public async Task RemovePerson(Person person)
        {
            if (modelPackage.AdultList.GetAdultById(person.Id)!=null)
            {
                modelPackage.AdultList.RemoveAdult(person);
                await DatabaseModelManager.RemoveAdult(person);
            }
            else if (modelPackage.ChildList.GetChildById(person.Id)!=null)
            {
                modelPackage.ChildList.RemoveChild(person);
                await DatabaseModelManager.RemoveChild(person);
            }
            foreach (var family in modelPackage.FamilyList.families)
            {
                family.Adults.RemoveAdult(person);
                family.Children.RemoveChild(person);
                await DatabaseModelManager.UpdateFamily(family);
            }
            UpdateData();
        }

        public ModelPackage GetModelPackage()
        {
            return modelPackage.Copy();
        }
    }
}