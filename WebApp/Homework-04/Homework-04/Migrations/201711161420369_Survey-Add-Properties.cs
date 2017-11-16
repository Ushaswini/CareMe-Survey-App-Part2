namespace Homework_04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyAddProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surveys", "FrequencyOfNotifications", c => c.Int(nullable: false));
            AddColumn("dbo.Surveys", "Time1", c => c.String());
            AddColumn("dbo.Surveys", "Time2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surveys", "Time2");
            DropColumn("dbo.Surveys", "Time1");
            DropColumn("dbo.Surveys", "FrequencyOfNotifications");
        }
    }
}
