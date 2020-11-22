using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.DbUnit
{
    public class DbChildFamily
    {
        public string Address { get; set; }
        //[JsonIgnore]
        public DbFamily Family { get; set; }
        public int Id { get; set; }
        //[JsonIgnore]
        public DbChild Child { get; set; }
    }
}