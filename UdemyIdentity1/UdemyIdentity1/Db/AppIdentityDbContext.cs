using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyIdentity1.Models;
using UdemyIdentity1.Models.ViewModels;

namespace UdemyIdentity1.Db
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
        public DbSet<UdemyIdentity1.Models.ViewModels.UserViewModel> UserViewModel { get; set; }

    }
}
