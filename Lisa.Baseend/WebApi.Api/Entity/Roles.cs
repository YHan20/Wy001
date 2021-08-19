using System;
using System.Collections.Generic;

namespace WebApi.Api.Entity
{
    public class Roles : BaseEntity
    {
        public string RoleName { get; set; }
        public virtual IEnumerable<UserRoles> UserRoles { get; set; }
    }
}