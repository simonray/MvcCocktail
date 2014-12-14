using MvcCocktail.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Entities.Configuration
{
    class UserMapper : EntityTypeConfiguration<AppUser>
    {
        public UserMapper()
        {
            this.ToTable("Users");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.Email).IsRequired();
            this.Property(s => s.Email).HasMaxLength(512);
        }
    }
}
