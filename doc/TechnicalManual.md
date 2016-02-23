# Description

Project is developed in ASP.NET in C#, using Telerik Kendo UI MVC wrappers.

# Project areas

### Public part
List details for started projects
- HomeController (JsonController)
- Index view, _Projects partial view
- HomeView and ProjectHomeView models

### Common autorized part
Profile change
- ProfileController, AccountController
- Index view
- UserViewModel

Messages sending/receiving
- MessagesController
- Inbox, Details, Send view
- MessageViewModel, MessageUserViewModel, MessageCreateViewModel

### Manager area
- BaseController, JsonController, IndexController, ProjectsController
- Index, Create, Details view, _DetailsFiles, _DetailsChart partial views
- ProjectListViewModel, ProjectListViewModel, ProjectCreateViewModel, ProjectDetailsResourseViewModel, ProjectDetailsDependencyViewModel, ProjectDetailsAssignmentsViewModel

### Lead area
- BaseController, JsonController, IndexController, ProjectsController, EstimationsController
- Index, Create, Details view, _DetailsFiles, _DetailsChart partial views
- EstimationListViewModel, EstimationEditViewModel, ProjectDetailsResourseViewModel, ProjectDetailsDependencyViewModel, ProjectDetailsAssignmentsViewModel

### HR area
- BaseController, JsonController, IndexController, SkillsController, UsersCotroller
- Skill Index, User Index, User details view
- SkillViewModel, UserEditViewModel, UserViewModel

### Worker area
- BaseController, JsonController, AssignmentsController
- Assignments view
- AssignmentViewModel

# General Requirements Covered
- 26 controllers
- 60 actions
- Razor view engine with Kendo UI wrappers 
- ~20 Editor and Display templates
- Entity framework 6 with SQL Server DB
- Repository pattern with data services
- 4 area user (Manager, Lead, HR and worker)
- All data visualization is based on JSON requests with server side paging/sorting
- Bootstrap/Kendo vizualization
- Possibility to change themes for each user
- Toastr notifications used
- ASP Identity system used, 4 different roles predifined
- Data caching used for home page and internal server logic
- Used Autofac dependency injector for MVC5
- Automapper for model mappings
- Site completely protected against XSS, XSRF, Parameter Tampering, HTML sanitizer used
- Server and client validation implemented on all pages (error messages localized)
- Used GitHub as a source repository
- 30 Unit Tests (Routes and Services)
- MD User and techical documentation
- 0 StyleCop warnings

# Additional Requirements Covered
- Project hosted on Windows Server 2016 machine with IIS 10 
[link](http://atanas.it)
- Project completely localized in English and Bulgarian based on Browser's language
