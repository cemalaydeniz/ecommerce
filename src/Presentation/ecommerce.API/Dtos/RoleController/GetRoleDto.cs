namespace ecommerce.API.Dtos.RoleController
{
    public class GetRoleDto
    {
        public string Name { get; set; } = null!;
        public List<UserInfo> Users { get; set; } = null!;

        public class UserInfo
        {
            public string Email { get; set; } = null!;
            public string? Name { get; set; }
        }
    }
}
