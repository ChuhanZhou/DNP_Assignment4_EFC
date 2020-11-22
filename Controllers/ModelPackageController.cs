using System;
using System.Text.Json;
using System.Threading.Tasks;
using DNP_Assignment4_EFC.Data;
using DNP_Assignment4_EFC.Models;
using DNP_Assignment4_EFC.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DNP_Assignment4_EFC.Controllers
{
    [ApiController]
    [Route("api/all")]
    public class ModelPackageController : ControllerBase
    {
        private IModelManager modelManager;

        public ModelPackageController()
        {
            modelManager = ModelManager.GetModelManager();
        }

        [HttpGet]
        public async Task<ActionResult<ModelPackage>> GetAllData()
        {
            ModelPackage modelPackage = modelManager.GetModelPackage();
            try
            {
                return Ok(JsonSerializer.Serialize(modelPackage,new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}