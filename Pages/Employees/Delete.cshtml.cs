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
    public class DeleteModel : PageModel
    {
        private readonly RPCClient _rpcClient;

        public DeleteModel(RPCClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //Employee = await _context.Employee.FindAsync(id);
            Employee = _rpcClient.Find<Employee>(id);
            if (Employee != null)
            {
                _rpcClient.Remove(id);

                //_context.Employee.Remove(Employee);
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
