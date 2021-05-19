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
    public class DetailsModel : PageModel
    {
        //private readonly WebApplication1.Data.EmployeeContext _context;
        private readonly RPCClient _rpcClient;

        public DetailsModel(RPCClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Employee = await _context.Employee.FirstOrDefaultAsync(m => m.ID == id);

            Employee = _rpcClient.Find<Employee>(id);

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
