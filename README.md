# MVCWebsite
A blog of a non blogger.

This project is an exercise of building an application using .NET MVC architecture and Azure deployment.

The application developed using C#, Razor, JavaScript, jQuery, Bootstrap4 and SQL.

The exercise also demonstrate how to use jQuery ajax to send and receiv JSON to/from Controller using built in verification token.

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

to Razor @Html helper

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

11. use IWebHostEnvironment when working with file using .NET core. webRootHost will return the wwwwroot/ folder.

12. IFormFile is the type you want to use in the Controller parameter when working with file upload.

### tricky stuffs
1. I move scripts tags below head tags in _Layout.cshtml. This will making sure jQueries are loaded before our scripts.

2. pass Verification token in the jQuery ajax header. Like so:
```
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    if (StikyText || StikyText.length > 4) {
        $.ajax({
            url: "/Boards/NewStiky",
            headers: {
                "RequestVerificationToken": token
            },
```

and in Razor page add:

```
@section Scripts {
    @using (Html.BeginForm(null, null, FormMethod.Post, new { id = "__AjaxAntiForgeryForm" }))
    {
        @Html.AntiForgeryToken()
    }
}
```

3. pass ajax data as object and received it in the Controller as a Model or Object with the same properties.
```
            type: "POST",
            data: JSON.stringify({
                Text: StikyText,
                BoardId: id
            }),
```

and my Stiky class:

```
    public class Stiky
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
        // $.ajax JSON object follows these properties 
        public string Text { get; set; }
        public int BoardId { get; set; }
        //
        public virtual Board Board { get; set; }

        public Stiky()
        {
            Type = "text";
            CreatedAt = DateTime.Today;
        }
    }
```

in the controller : in the function parameter add [FromBody] Object obj !!!

return it as JsonResult object

```
        // POST: Boards/NewStiky
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewStiky([FromBody] Stiky stiky)
        {
            string text = stiky.Text;
            if (!string.IsNullOrEmpty(text) && text.Length > 4 && text.Length < 255)
            {
                Stiky newS = new Stiky()
                {
                    Text = stiky.Text,
                    BoardId = stiky.BoardId
                };

                _context.Add(newS);
                await _context.SaveChangesAsync();
            };

            IEnumerable<Stiky> stikies = from s in _context.Stiky
                                             where s.BoardId == stiky.BoardId
                                             select s;

            JsonResult res = new JsonResult(stikies);

            return (res);
        }
```

4. The above method create a problem. In the AJAX I use JSON.Stringify that convert the data to string. 

The Controller don't accept it because the data doesn't match one of the integer object's property.

---Solution---: create a dummy class that have string properties. Convert to other type as neccessary.

Clipboard

@using (Html.BeginForm("NewStikyImage", "Boards", FormMethod.Post, new { enctype = "multipart/form-data" }))
                    {
                        @*@Html.ValidationSummary();*@

                        @*input box and label and warning span *@
                        <!--<input type='file' class="custom-file-input" id="newStikyPic" name="newStikyPic" />
                        <label class="custom-file-label" for="customFile">Choose file</label>
                        <span class="text-danger" id="spanfile"></span>-->

                         @*img preview before upload *@
                        <img class="med-image" src="\images\empty_pic.png" alt="UploadedImage" id="newStikyPicPreview" />

                         @*upload button *@
                        <input type="submit" id="btnUpload" class="btn btn-sm btn-dark" value="Upload" />
                    }