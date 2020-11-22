using System.Text.Json.Serialization;
using DNP_Assignment4_EFC.Models.Unit;

namespace DNP_Assignment4_EFC.Models.DbUnit
{
    public class DbAdultFamily
    {
        public string Address { get; set; }
        public DbFamily Family { get; set; }
        public int Id { get; set; }
        public Adult Adult { get; set; }
    }
}