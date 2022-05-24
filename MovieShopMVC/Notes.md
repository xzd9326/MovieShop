MVC -> Model 
View 
Controller -> a C# class that inherits from Controller class
			-> Many Action methods

return View() will return the view with the same name as action method name
return View("someviewname") also

Views in MVC are called Razor view with cshtml extension -> combine C# and HTML code

HTTP Requests ->
GET => top 30 movies, get user by id, get movies for genre is 3
POST => Register User, Login
PUT => Edit Profile
DELETE => Delet account

Get User Info
GET https://localhost:7211/user/details/1
1. UserController -> Controllers
2. Create Details action method -> GET
3. User folder inside Views
4. Create details.cshtml

Organize our code properly
Code should be reusable code and easily testable

Entities represents your Database tables
Movie table => Movie class columns will be mapped with properties
Genre => Genre class

Models/ViewModels/DTO (Data Transfer Objects) => Views
Home Page => MovieCardModel => PosterUrl, Id, Title
You create model classes based on the requirement of the view
(model used for view, entity used for database)

Repositories classes/interfaces they deal with Entity classes
Services classes/interfaces they deal with Model classes


Every View before rendering, it will inherit view start and => Layout

MovieCard view in multiple views
Home/index -> MovieCard
User/purchases => MovieCard
User/Favorites => MovieCard

Partial View => MovieCardPartial and then you can reuse it across multiple views

Dependency Injection
Design Pattern that enables you to write loosely coupled code
tightly coupled code vs loosely coupled

easy to maintain
easy to test
easy to change the functionality without changing much of the code


method(int x, IMovieService service)

var movieService = new MovieService();
var movieService2 = new MovieTestService();

method(5, movieService)

HomeController c = new HomeController();


### EF Core Code First Approach using Migrations

1. Create an Entity that you need based on Table requirement
1. Establish the connection string where you want the database to be created
1. Install required libraries from NuGet
1. DbContext -> Represents your database and DbSet -> represents your table
1. Create Custom DbContext class that inherits from DbContext base class
1. Inject DbContextOptions from Program.cs with connection string into DbContext
1. Create DbSet<Entity> property inside the DbContext
1. add-migration
1. update-database
1. Check the SQL Server if the databse and table are created
1. Do not change the Database schema maneally, always go from Code and anpply new migration
1. Two ways to model our Code first design
	1. Data Annotations
	1. Fluent API takes
