using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class ProjectRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Project>> Get()
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Project>>("/api/projects");
        }
    }
}