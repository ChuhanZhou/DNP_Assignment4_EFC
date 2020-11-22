using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.DbUnit
{
    public class DbChildInterest
    {
        public int Id { get; set; }
        //[JsonIgnore]
        public DbChild Child { get; set; }
        public string Type { get; set; }
        //[JsonIgnore]
        public Interest Interest { get; set; }
    }
}