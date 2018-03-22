using System.Data.Entity.ModelConfiguration;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Configuration
{
    public class ClientProfileConfiguration : EntityTypeConfiguration<ClientProfile>
    {
        public ClientProfileConfiguration()
        {
            Property(p => p.FullName).IsMaxLength();
            Property(p => p.CreateDate).IsRequired().HasColumnType("datetime2");
            Property(p => p.LastVisitDate).IsRequired().HasColumnType("datetime2");
        }
    }
}
