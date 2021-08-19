using System;
using System.Collections.Generic;

namespace WebApi.Api.Entity
{
    public class Users : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual IEnumerable<UserRoles> UserRoles { get; set; }
    }
}