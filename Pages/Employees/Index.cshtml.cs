using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DataTransmission;
using WebApplication1.Models;

namespace WebApplication1.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly RPCClient _rpcClient;

        public IndexModel(RPCClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public IList<Employee> Employee { get;set; }

        public async Task OnGetAsync()
        {
            Employee = _rpcClient.ToListAsync<Employee>();
        }
    }
}
