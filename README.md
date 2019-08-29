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
## Configuration and Initial Visual Studio Setup

 1. You will need to modify the database connection string in
    **application.json** within the **MarksMovies.RazorPagesUI** project.
    
    The default value to use would be:
    
         "ConnectionStrings": {
            "MarksMoviesContext": "Server=(localdb)\\mssqllocaldb;Database=MarksMovies;MultipleActiveResultSets=true"
          }
 2. Build the solution.
 3. Open up the **Package Manager Console** and then enter the command
    **Update-Database**.
 4. You should now be able to run the application.

## Seeding the database
There is a file located at **/Assets/Movies.json** in the **MarksMovies.RazorPagesUI** project which, if present, will be used to seed the database with movies.  The same file that is generated through the application's json export feature may be used to seed an empty database.
# Features
From the main page of the web application you can choose Movies or Swagger
## Movies
### Index page
This page shows the full list of movies in the catalog.  From here you can choose to Edit, Delete, or view the Details for any existing movie.  You may also Create a new movie, Rank the list of movies, or Export the catalog to a file.
### Create and Edit pages
These pages are very similar and support entering data for a movie or television item.  There is also a **Fetch TMDB Data** button which will allow you to **Import** data directly from **The Movie Database** on the web.  Here is what the flow would look like on these pages:

 1. Enter a title as a search string
 2. Choose Movie or TV Show option
 3. Click the Fetch TMDB Data button
 4. Click on the Import button for the item that best matches your
    search
 5. Make any additional edits as needed and click Save/Create

Please note that the Fetch TMDB Data button will make different API calls behind the scenes depending on your Movie or TV Show selection.
### Details page
This page shows details obtained from **The Movie Database** including an overview, a poster image, and the top 5 cast members, along with other details.
### Delete page
A movie can be deleted from this page
### Rank Movies page
This page consists of a simple listing of all the movies in the catalog (excluding TV Shows).  The list supports drag and drop for re-ordering according to personal preference.  When you are done ranking movies you can **Save** the rank values to the database.
### Export page
This page currently supports exporting the list of movies from the catalog to Excel or to Json.
## Swagger
This is an auto-generated Swagger page for testing the API endpoints for this application.
