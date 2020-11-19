using System;
using System.Text.Json;
using System.Threading.Tasks;
using DNP_Assignment4_EFC.Models;
using DNP_Assignment4_EFC.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DNP_Assignment4_EFC.Controllers
{
    [ApiController]
    [Route("api/all")]
    public class ModelPackageController : ControllerBase
    {
        private ModelPackage modelPackage;

        public ModelPackageController()
        {
            modelPackage = new ModelPackage();
        }

        [HttpGet]
        public async Task<ActionResult<ModelPackage>> GetAllData()
        {
            try
            {
                DataFileContext.ReadData("DataFile.json",modelPackage);
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