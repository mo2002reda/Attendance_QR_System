using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Entity.IdentityEntity
{
    public class StudentApp : IdentityUser
    {
        public bool IsActive { get; set; }
        //public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        //public string Email { get; set; }
        public string StudentId { get; set; }
    }
}
