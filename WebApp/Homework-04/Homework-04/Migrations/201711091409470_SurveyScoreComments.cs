namespace Homework_04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyScoreComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.SurveyResponses", "SurveyComments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyResponses", "SurveyComments");
            DropColumn("dbo.AspNetUsers", "Gender");
        }
    }
}
