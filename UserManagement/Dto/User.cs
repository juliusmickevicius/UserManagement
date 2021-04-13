using UserManagement.Dto.Enums;

namespace UserManagement.Dto
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public Duties Duties { get; set; }
    }
}
