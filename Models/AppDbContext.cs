using Microsoft.EntityFrameworkCore;

namespace QuillDemo.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>().HasData
                (
                    new Document
                    { 
                        Id = 1,
                        DeltaJson = @"[{""insert"":""This is Quill""},{""attributes"":{""header"":1},""insert"":""\n""},{""attributes"":{""size"":""large""},""insert"":""It's pretty cool.""},{""insert"":""\nSupports bulleted or numbered lists""},{""attributes"":{""list"":""ordered""},""insert"":""\n""},{""attributes"":{""bold"":true},""insert"":""Bold, ""},{""attributes"":{""italic"":true},""insert"":""italic,""},{""insert"":"" ""},{""attributes"":{""underline"":true},""insert"":""underline""},{""insert"":"" etc and ""},{""attributes"":{""link"":""http://google.com""},""insert"":""links""},{""attributes"":{""list"":""ordered""},""insert"":""\n""},{""attributes"":{""color"":""#9933ff""},""insert"":""Colors as well""},{""attributes"":{""list"":""ordered""},""insert"":""\n""},{""insert"":""\nQuill > ""},{""attributes"":{""italic"":true},""insert"":""<input type=\""text\"" />""},{""insert"":""\n\n""},{""attributes"":{""color"":""#008a00"",""size"":""large"",""bold"":true},""insert"":""Can make text large, bold, and colored like this""},{""insert"":""\n\nIt also supports images, although one of my browsers (and only one) caused the app to crash when doing so. Not sure why. Hopefully not an issue in production. Your mileage may vary.\n\n""},{""attributes"":{""size"":""huge"",""link"":""/home/edit""},""insert"":""Go edit this page""},{""insert"":""\n\n""},{""attributes"":{""size"":""small""},""insert"":""(currently, everything is wrapped in a ""},{""attributes"":{""font"":""monospace"",""size"":""small""},""insert"":"".ql-editor ""},{""attributes"":{""size"":""small""},""insert"":""class to preserve styles from the editor. this could be customized, but may involve a lot of custom css depending on how many features are enabled in the editor.)""},{""insert"":""\n""}]"
                    }
                );

            Console.WriteLine("done");
        }

    }
}
