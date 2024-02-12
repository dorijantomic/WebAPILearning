using App.Repository;
using App.Repository.ApiClient;
using System;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:44332", httpClient);

await GetProjects();

async Task GetProjects()
{
    ProjectRepository repository = new(apiExecuter);
    var projects = await repository.Get();
    foreach (var project in projects)
    {
        Console.WriteLine($"Project: {project.Name}");
    }
}