namespace DashBoard.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtoApplicantNameFound : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "NameFound", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicant", "NameFound");
        }
    }
}
