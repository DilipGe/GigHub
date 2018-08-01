namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Jazz')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Gazal')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Country')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Rock')");
        }
        
        public override void Down()
        {
            Sql("DELETE from Genres where Id in (1,2,3,4)");
        }
    }
}
