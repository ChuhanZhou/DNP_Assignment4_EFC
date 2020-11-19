using System.IO;
using System.Text.Json;
using DNP_Assignment4_EFC.Models;

namespace DNP_Assignment4_EFC.Persistence
{
    public class DataFileContext
    {
        public static void ReadData(string dataFileName,ModelPackage modelPackage)
        {
            if (!File.Exists(dataFileName))
            {
                UpdateData(dataFileName,modelPackage);
            }
            string json = File.ReadAllText(dataFileName);
            var read = JsonSerializer.Deserialize<ModelPackage>(json);
            modelPackage.UserList = read.UserList;
            modelPackage.AdultList = read.AdultList;
            modelPackage.ChildList = read.ChildList;
            modelPackage.FamilyList = read.FamilyList;
        }

        public static void UpdateData(string dataFileName,ModelPackage modelPackage)
        {
            string json = JsonSerializer.Serialize(modelPackage,new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(dataFileName,json);
        }
    }
}