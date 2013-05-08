BlogMVC
=======

-- BLOG MVC CODE FIRST EF --

Origional idea from : https://github.com/VJAI/JustBlog
Vijaya Anand, but ofcourse totally refactored it.

Used:

- EF, code first.
- Migrations.
- MVC.
- OpenSource Kendo UI Web.
- Toastr (messaging).
- AutoMapper.
- Etc.

Getting started :

- Download and unzip files

- Enable NuGet, VisualStudio2012 Tools->Package Manager->Enable Package Restore

- Added controller for register user for admin section.
For registering user uncomment section : 

App_Start->RouteConfig.cs       
//For register user purpose     
//routes.MapRoute(      
//    name: "Default",  
//    url: "{controller}/{action}/{id}",        
//    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }       
//);    

Navigate to : http://localhost:[someport]/Account/Register	
Add username an password and confirm pasword	
The redirect will fail but that's oke, you now have a user.	
Now comment the section again ( App_Start->RouteConfig.cs ) else the pages will fail to load.	
	
- Adding some data to play with :)	
First restart you VisualStudio 2012, for reasons unknown data migrations won't always work.	
Uncomment line :	
	
Migrations->Configuration.cs	
	
protected override void Seed(BlogContext context)	
{	
  //  This method will be called after migrating to the latest version.	
	//context.Seed();	
}	
	
And comment the following line (else you will have duplicate data) :	
	
Project Blog.Data BlogContext.cs	
	
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
	// Use singular table names
	modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
	modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); //Todo WillCascadeOnDelete(false)

	SetupCategoryEntity(modelBuilder);
	SetupContactEntity(modelBuilder);
	SetupMembershipEntity(modelBuilder);
	SetupPostEntity(modelBuilder);
	SetupRoleEntity(modelBuilder);
	SetupTagEntity(modelBuilder);
	SetupUserEntity(modelBuilder);

	// EF (4.3 and up) auto-migrations initializer
	->> Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, Configuration>()); <<-
}

Then use the Package Manager Console and choose the correct project -> Blog.Data
type -> Update-Database -Verbose
And hit enter.

After that the database file will be seeded.	
Now reverse the uncomment and comment part so you won't keep seeding the database file and get diplicate data.	

- Navigate to http://localhost:[someport]/Admin		

Login with the earlier created user.

-- TODO --
- Admin posts editor is not complete. No way the add and display Tags at this point.
- Some way to seed an admin user initially.
- Add users edit grid in Admin.
- New part fo adding comment to post and admin part to admin comments before granting.


With Kind Regards,

Frank van der geld

PS. IF you want to help me complete or extend this project please contact me.
