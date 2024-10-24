namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public List<int> Permissions { get; set; }
        public AuthViewModel()
        {
        }
        public AuthViewModel(long id, long roleId, string role, string fullname, string username, List<int> permissions, string mobile)
        {
            Id = id;
            RoleId = roleId;
            Role = role;
            Fullname = fullname;
            Username = username;
            Permissions = permissions;
            Mobile = mobile;
        }
    }
}