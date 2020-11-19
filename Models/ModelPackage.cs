using DNP_Assignment4_EFC.Models.List;

namespace DNP_Assignment4_EFC.Models
{
    public class ModelPackage
    {
        public UserList UserList { get; set; }
        public FamilyList FamilyList { get; set; }
        public AdultList AdultList { get; set; }
        public ChildList ChildList { get; set; }
        public ModelPackage()
        {
            UserList = new UserList();
            FamilyList = new FamilyList();
            AdultList = new AdultList();
            ChildList = new ChildList();
        }
    }
}