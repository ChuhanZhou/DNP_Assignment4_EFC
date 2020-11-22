using System.Threading.Tasks;
using DNP_Assignment4_EFC.Models;
using DNP_Assignment4_EFC.Models.List;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Data
{
    public interface IModelManager
    {
        Task<string> AddUser(User newUser);
        bool Login(User user);
        UserList GetAllUser();
        Task<string> UpdatePassword(User oldUser,User newUser);
        Task RemoveUser(User user);
        Task<string> AddFamily(Family newFamily);
        FamilyList GetAllFamily();
        Task<string> UpdateFamily(Family oldFamily, Family newFamily);
        Task RemoveFamily(Family family);
        Task<string> AddAdult(Adult newAdult);
        AdultList GetAllAdult();
        Task<string> AddChild(Child newChild);
        ChildList GetAllChild();
        Task<string> UpdatePerson(Person newPerson);
        Task RemovePerson(Person person);
        ModelPackage GetModelPackage();
    }
}