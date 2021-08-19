using System;

namespace WebApi.Api.Entity
{
    public class UserRoles : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Users User { get; set; }
        public virtual Roles Role { get; set; }
    }
}