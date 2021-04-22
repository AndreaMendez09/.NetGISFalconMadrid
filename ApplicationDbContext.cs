using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net_Gis_Falcon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Net_Gis_Falcon.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UsuariosModelo> Usuarios { get; set; }
    }
}
