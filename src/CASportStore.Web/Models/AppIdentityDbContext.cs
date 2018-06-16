using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASportStore.Web.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        // authenticating users and authorizing access to application features and data
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) 
            : base(options) { }
    }
}
