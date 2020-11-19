using System;
using System.Text.Json;
using System.Threading.Tasks;
using DNP_Assignment4_EFC.Data;
using DNP_Assignment4_EFC.Models.List;
using DNP_Assignment4_EFC.Models.Unit;
using DNP_Assignment4_EFC.Data;
using Microsoft.AspNetCore.Mvc;

namespace DNP_Assignment4_EFC.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonListController : ControllerBase
    {
        private IModelManager modelManager;

        public PersonListController()
        {
            modelManager = ModelManager.GetModelManager();
        }
        
        [HttpGet]
        public async Task<ActionResult<PersonList>> GetAllPerson()
        {
            try
            {
                var personList = new PersonList();
                foreach (var adult in modelManager.GetAllAdult().GetAllWithList())
                {
                    personList.AddPerson(adult);
                }
                foreach (var child in modelManager.GetAllChild().GetAllWithList())
                {
                    personList.AddPerson(child);
                }
                return Ok(JsonSerializer.Serialize(personList,new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult<string>> UpdatePerson([FromBody] string newPersonJson)
        {
            try
            {
                var newPerson = JsonSerializer.Deserialize<Person>(newPersonJson);
                if (modelManager.GetAllAdult().GetAdultById(newPerson.Id)!=null)
                {
                    newPerson = JsonSerializer.Deserialize<Adult>(newPersonJson);
                }
                else if (modelManager.GetAllChild().GetChildById(newPerson.Id)!=null)
                {
                    newPerson = JsonSerializer.Deserialize<Child>(newPersonJson);
                }
                return modelManager.UpdatePerson(newPerson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task RemovePerson([FromQuery] int id)
        {
            try
            {
                if (modelManager.GetAllAdult().GetAdultById(id)!=null)
                {
                    modelManager.RemovePerson(modelManager.GetAllAdult().GetAdultById(id));
                }
                else if (modelManager.GetAllChild().GetChildById(id)!=null)
                {
                    modelManager.RemovePerson(modelManager.GetAllChild().GetChildById(id));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}