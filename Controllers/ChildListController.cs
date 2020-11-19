using System;
using System.Text.Json;
using System.Threading.Tasks;
using DNP_Assignment4_EFC.Data;
using DNP_Assignment4_EFC.Models.List;
using DNP_Assignment4_EFC.Models.Unit;
using Microsoft.AspNetCore.Mvc;

namespace DNP_Assignment4_EFC.Controllers
{
    [ApiController]
    [Route("api/person/child")]
    public class ChildListController : ControllerBase
    {
        private IModelManager modelManager;

        public ChildListController()
        {
            modelManager = ModelManager.GetModelManager();
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> AddChild([FromBody] Child newChild)
        {
            try
            {
                return modelManager.AddChild(newChild);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<AdultList>> GetAllChild()
        {
            try
            {
                var childList = modelManager.GetAllChild();
                return Ok(JsonSerializer.Serialize(childList,new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}