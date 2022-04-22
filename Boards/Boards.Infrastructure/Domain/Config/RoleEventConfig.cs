using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boards.Domain.Roles;
using DI.Domain;
using DI.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boards.Infrastructure.Domain.Config
{
    public class RoleEventConfig : IEntityTypeConfiguration<BoardRoleEvent>
    {
        public void Configure(EntityTypeBuilder<BoardRoleEvent> builder)
        {


        }
    }
}
