# MarksMovies Projects
There are currently 6 projects in this solution:

 - **MarksMovies.RazorPagesUI** -- Front end web UI
 - **MarksMovies.DataModel** --- Core classes for the data model which map to database tables
 - **MarksMovies.DataAccess** --- DB Context, Migrations, and general data access layer
 - **MarksMovies.TMDB** --- The Movie Database API methods and objects
 - **MarksMovies.Services** --- Business logic
 - **MarksMovies.Tests** --- Unit tests

## Main project is MarksMovies.RazorPagesUI
This is the front end web project built using Razor Pages in ASP.Net Core.  This is also where you will find the Program.cs, Startup.cs and application.json files.
## Configuration
You will need to modify the database connection string in **application.json** within the **MarksMovies.RazorPagesUI** project.

The default value to use would be:

     "ConnectionStrings": {
        "MarksMoviesContext": "Server=(localdb)\\mssqllocaldb;Database=MarksMovies;MultipleActiveResultSets=true"
      }
## Seeding the database
There is a file located at **/Assets/Movies.json** in the **MarksMovies.RazorPagesUI** project which, if present, will be used to seed the database with movies.  The same file that is generated through the application's json export feature may be used to seed an empty database.
