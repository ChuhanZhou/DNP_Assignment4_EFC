using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNP_Assignment4_EFC.Models.DbUnit;
using DNP_Assignment4_EFC.Models.List;
using DNP_Assignment4_EFC.Models.Unit;
using Microsoft.EntityFrameworkCore;

namespace DNP_Assignment4_EFC.DataAccess
{
    public class DatabaseModelManager
    {
        public static async Task AddUser(User newUser)
        {
            if (newUser!=null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    await dbContext.Users.AddAsync(newUser);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task<UserList> GetAllUser()
        {
            try
            {
                var dbContext = new AssignmentDbContext();
                UserList userList= new UserList();
                foreach (var user in await dbContext.Users.ToListAsync())
                {
                    userList.AddUser(user);
                }
                return userList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task UpdateUser(User newUser)
        {
            if (newUser!=null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    dbContext.Users.Update(newUser);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task RemoveUser(User user)
        {
            if (user != null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    User result = dbContext.Users.First(i => i.UserName == user.UserName);
                    dbContext.Users.Remove(result);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public static async Task AddAdult(Adult newAdult)
        {
            if (newAdult!=null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    await dbContext.Adults.AddAsync(newAdult);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task<AdultList> GetAllAdult()
        {
            try
            {
                var dbContext = new AssignmentDbContext();
                AdultList adultList= new AdultList();
                foreach (var adult in await dbContext.Adults.ToListAsync())
                {
                    adultList.AddAdult(adult);
                }
                return adultList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task UpdateAdult(Adult newAdult)
        {
            if (newAdult!=null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    dbContext.Adults.Update(newAdult);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task RemoveAdult(Person adult)
        {
            if (adult != null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    Adult result = dbContext.Adults.First(i => i.Id == adult.Id);
                    dbContext.Adults.Remove(result);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public static async Task AddChild(Child newChild)
        {
            if (newChild!=null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    await dbContext.Children.AddAsync(newChild.ToDb());
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task<ChildList> GetAllChild()
        {
            try
            {
                var dbContext = new AssignmentDbContext();
                ChildList childList= new ChildList();
                List<DbChild> children = await dbContext.Children.ToListAsync();
                foreach (var child in children)
                {
                    child.ChildInterest = await GetChildInterests(child.Id);
                    child.Pets = await GetPets(child.Id);
                    childList.AddChild(child.ToChild());
                }
                return childList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task<List<DbChildInterest>> GetChildInterests(int id)
        {
            var dbContext = new AssignmentDbContext();
            List<DbChildInterest> childInterests = await dbContext.Children.Where(c => c.Id == id).SelectMany(c=>c.ChildInterest).ToListAsync();
            foreach (var childInterest in childInterests)
            {
                childInterest.Interest = await GetInterest(childInterest.Type);
            }
            return childInterests;
        }

        private static async Task<Interest> GetInterest(string type)
        {
            var dbContext = new AssignmentDbContext();
            return await dbContext.Interests.FindAsync(type);
        }
        
        public static async Task UpdateChild(Child newChild)
        {
            Console.WriteLine(2);
            if (newChild!=null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    FamilyList families = await GetAllFamily();
                    await RemoveChild(newChild);
                    await AddChild(newChild);
                    foreach (var family in families.families)
                    {
                        if (family.Children.GetChildById(newChild.Id)!=null)
                        {
                            await UpdateFamily(family);
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task RemoveChild(Person child)
        {
            try
            {
                if (child != null)
                {
                    var dbContext = new AssignmentDbContext();
                    DbChild result = await GetChild(child.Id);
                    foreach (var pet in result.Pets)
                    {
                        await RemoveChildPet(pet.PetId);
                    }
                    result = await GetChild(child.Id);
                    dbContext.Children.Remove(result);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task RemoveChildPet(string petId)
        {
            var dbContext = new AssignmentDbContext();
            Pet result = await dbContext.Children.SelectMany(c=>c.Pets).FirstAsync(p=>p.PetId==petId);
            dbContext.Remove(result);
            await dbContext.SaveChangesAsync();
        }
        
        private static async Task RemoveFamilyPet(string petId)
        {
            var dbContext = new AssignmentDbContext();
            Pet result = await dbContext.Families.SelectMany(c=>c.Pets).FirstAsync(p=>p.PetId==petId);
            dbContext.Remove(result);
            await dbContext.SaveChangesAsync();
        }
        
        public static async Task AddFamily(Family newFamily)
        {
            try
            {
                if (newFamily!=null)
                {
                    var dbContext = new AssignmentDbContext();
                    await dbContext.Families.AddAsync(newFamily.ToDb());
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<FamilyList> GetAllFamily()
        {
            try
            {
                var dbContext = new AssignmentDbContext();
                FamilyList familyList= new FamilyList();
                foreach (var family in await dbContext.Families.ToListAsync())
                {
                    family.AdultFamily = await GetAdultFamilies(family.Address);
                    family.ChildFamily = await GetChildFamilies(family.Address);
                    family.Pets = await GetPets(family.Address);
                    familyList.AddFamily(family.ToFamily());
                }
                return familyList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task<List<DbAdultFamily>> GetAdultFamilies(string address)
        {
            var dbContext = new AssignmentDbContext();
            List<DbAdultFamily> adultFamilies = await dbContext.Families.Where(f => f.Address == address)
                .SelectMany(f => f.AdultFamily).ToListAsync();
            foreach (var adultFamily in adultFamilies)
            {
                adultFamily.Adult = await GetAdult(adultFamily.Id);
            }
            return adultFamilies;
        }
        
        private static async Task<List<DbChildFamily>> GetChildFamilies(string address)
        {
            var dbContext = new AssignmentDbContext();
            List<DbChildFamily> childFamilies = await dbContext.Families.Where(f => f.Address == address)
                .SelectMany(f => f.ChildFamily).ToListAsync();
            foreach (var childFamily in childFamilies)
            {
                childFamily.Child = await GetChild(childFamily.Id);
            }
            return childFamilies;
        }

        private static async Task<Adult> GetAdult(int id)
        {
            var dbContext = new AssignmentDbContext();
            return await dbContext.Adults.FindAsync(id);
        }
        
        private static async Task<DbChild> GetChild(int id)
        {
            var dbContext = new AssignmentDbContext();
            DbChild result = await dbContext.Children.FindAsync(id);
            result.ChildInterest = await GetChildInterests(result.Id);
            result.Pets = await GetPets(id);
            return result;
        }
        
        private static async Task<List<Pet>> GetPets(int childId)
        {
            var dbContext = new AssignmentDbContext();
            return await dbContext.Children.Where(c=>c.Id==childId).SelectMany(c=>c.Pets).ToListAsync();
        }
        
        private static async Task<List<Pet>> GetPets(string address)
        {
            var dbContext = new AssignmentDbContext();
            return await dbContext.Families.Where(f=>f.Address==address).SelectMany(f=>f.Pets).ToListAsync();
        }

        public static async Task UpdateFamily(Family newFamily)
        {
            if (newFamily!=null)
            {
                await RemoveFamily(newFamily);
                await AddFamily(newFamily);
                //try
                //{
                //    var dbContext = new AssignmentDbContext();
                //    dbContext.Families.Update(newFamily.ToDb());
                //    await dbContext.SaveChangesAsync();
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e);
                //    throw;
                //}
            }
        }

        public static async Task RemoveFamily(Family family)
        {
            if (family != null)
            {
                try
                {
                    var dbContext = new AssignmentDbContext();
                    foreach (var pet in await GetPets(family.ToDb().Address))
                    {
                        await RemoveFamilyPet(pet.PetId);
                    }
                    DbFamily result = dbContext.Families.First(i => i.Address == family.ToDb().Address);
                    dbContext.Families.Remove(result);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task SetAllInterests()
        {
            try
            {
                var dbContext = new AssignmentDbContext();
                foreach (var type in Interest.ValidInterest)
                {
                    await dbContext.Interests.AddAsync(new Interest
                    {
                        Type = type
                    });
                }
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}