using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.DAL.EF
{
    public class CreateDbIfNot : CreateDatabaseIfNotExists<DutContext>
    {
        protected override void Seed(DutContext context)
        {
            context.SeedAdminAccount("romeost", "123456");
        }
    }
}
