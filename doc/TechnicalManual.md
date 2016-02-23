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
- ~20 Editor na Display templates
