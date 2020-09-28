# MVCWebsite
A blog of a non blogger.

This project is an exercise of building an application using .NET MVC architecture and Azure deployment.

The application developed using C#, Razor and SQL.

The project was deployed to Azure App Service and the database is deployed to SQL Server.

.NET C# is a server end development. It is quite static and has built-in security features.

### tech-stack
- .NET C#
- SQL, LINQ
- Razor pages
- Entity framework core
- JavaScript
- Bootstrap 4
- Azure Web App Service
- Azure SQL Server
- dotnet CLI
- az CLI
- IIS Express

### project requirements
[Visual Studio 2019]
(https://visualstudio.microsoft.com/)

[dotnet CLI]
(https://dotnet.microsoft.com/download)

[az CLI]
(https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)

[Ms SQL Server management studio]
(https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)

### steps and libraries

1. Create a new project in MS Visual Studio 2019

2. Search for mvc and pick ASP.NET Core Web Application

3. It is all point and click from here except adding entity framework in the Package Manager console.

```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

4. Create a model in Models folder

You might need this modules

```
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
```

5. Create a context in Data folder

6. Set the database connection string in appsetting.json

```
"ConnectionStrings": {
    "MvcWebsiteContext": "Server=(localdb)\\mssqllocaldb;Database=PegBoard;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

7. After the model and Database connection set up

Use this commands to migrate and update the database

```
Add-Migration InitialCreate
Update-Database
```

8. Migration is a VCS for our database, you can change InitialCreate to something else in the future.

The scenario:
- You add/change a model
- You are not sure with it
- Add-Migration AddThisRowToThatTable
- It will capture model classes
- Revert back to a migration by name
```
Update-Database <previous-migration-name>
```
- Update-Database will record the new table / row information to the database

All commands were done in Package Manager Console -- PM>

9. Instead of using the scaffolding generated form.

Which look something like this:


```
<form method="post" asp-action="Create">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>
    <div class="form-group">
        <label asp-for="NewBoard.Title" class="control-label"></label>
        <input asp-for="NewBoard.Title" class="form-control" name="boardTitle" />
        <span asp-validation-for="NewBoard.Title" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="NewBoard.Text" class="control-label"></label>
        <textarea asp-for="NewBoard.Text" class="form-control" rows="4" name="boardText"></textarea>
        <span asp-validation-for="NewBoard.Text" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="NewBoard.Tags" class="control-label"></label>
        <input asp-for="NewBoard.Tags" class="form-control" name="boardTags" />
        <span asp-validation-for="NewBoard.Tags" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="NewBoard.BoardColor" class="control-label"></label>
        <select asp-for="NewBoard.BoardColor"
                asp-items="Model.BoardColors" class="custom-select" name="boardBGColor">
            <option>Select Board color</option>
        </select>
    </div>
    <div class="form-group">
        <input type="submit" value="Create" class="btn btn-dark btn-block" />
                @*<button class="btn btn-dark btn-block" onclick="letSee()"><img class="small-image" src="/images/save.png" alt="">&nbsp;create</button>*@
                @*<button class="btn btn-light btn-block" asp-action="Index"><img class="small-image" src="/images/to_boards.png" alt="">&nbsp;boards</button>*@
    </div>
</form>
```

I am using Razor @Html helper

```
@using (Html.BeginForm())
            {
                <fieldset>
                    @Html.HiddenFor(model => model.NewBoard.Id)
                    <div class="form-group">
                        <div class="editor-label">
                            @Html.LabelFor(model => model.NewBoard.Title)
                        </div>
                        <div class="editor-field">
                            @Html.TextBoxFor(model => model.NewBoard.Title, new { @class = "form-control" })
                            @Html.ValidationMessageFor(model => model.NewBoard.Title, "", new { @class = "text-danger" })
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="editor-label">
                            @Html.LabelFor(model => model.NewBoard.Text)
                        </div>
                        <div class="editor-field">
                            @Html.TextAreaFor(model => model.NewBoard.Text, new { @class = "form-control" })
                            @Html.ValidationMessageFor(model => model.NewBoard.Text, "", new { @class = "text-danger" })
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="editor-label">
                            @Html.LabelFor(model => model.NewBoard.Tags)
                        </div>
                        <div class="editor-field">
                            @Html.TextBoxFor(model => model.NewBoard.Tags, new { @class = "form-control" })
                            @Html.ValidationMessageFor(model => model.NewBoard.Tags, "", new { @class = "text-danger" })
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="editor-label">
                            @Html.LabelFor(model => model.NewBoard.BoardColor)
                        </div>
                        <div class="editor-field">
                            @Html.DropDownList("NewBoard.BoardColor", (SelectList)Model.BoardColors, new { @class = "form-control custom-select" })
                            @Html.ValidationMessageFor(model => model.NewBoard.BoardColor, "", new { @class = "text-danger" })
                        </div>
                    </div>
                    <div class="form-group">
                        <p>
                            <input type="submit" value="create" class="btn btn-dark btn-block" />
                            <button class="btn btn-light btn-block" asp-action="Index"><img class="small-image" src="/images/to_boards.png" alt="">&nbsp;boards</button>
                        </p>
                    </div>
                </fieldset>
            }
```

10. Don't forget to specify object's Id when update an entry

### triky stuffs
I move <scripts> below <head> in _Layout.cshtml. This will making sure jQueries are loaded before our scripts loaded